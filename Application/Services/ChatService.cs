using Application.DTOs.Chat;
using Application.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(Guid chatRoomId, int page = 1, int pageSize = 50)
        => await _chatRepository.GetMessagesAsync(chatRoomId, page, pageSize);

        public async Task<ChatRoom> GetOrCreatePrivateChatAsync(string userAClerkId, string userBClerkId, Guid petId)
        {
            var userPair = new List<string> { userAClerkId, userBClerkId };
            userPair.Sort(StringComparer.Ordinal);
            var first = userPair[0];
            var second = userPair[1]; var room = await _chatRepository.GetPrivateRoomAsync(first, second);
            if (room == null)
            {
                room = await _chatRepository.CreatePrivateRoomAsync(first, second, petId);
                await _chatRepository.SaveChangesAsync();
            }
            return room;
        }

        public async Task<ChatMessage> SendMessageAsync(Guid chatRoomId, string senderId, string recipientId, string content)
        {
            var message = new ChatMessage(chatRoomId, senderId, recipientId, content);
            try
            {
                await _chatRepository.AddMessageAsync(message);
                await _chatRepository.SaveChangesAsync();
                return message;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Result<ChatRoomDto>> GetChat(Guid chatRoomId)
        {
            var chatRoom = await _chatRepository.GetChat(chatRoomId);
            return Result<ChatRoomDto>.Ok(new ChatRoomDto(chatRoom));
        }

        public async Task<Result<List<ChatRoomDto>>> GetChats(string clerkId)
        {
            var chats = await _chatRepository.GetChats(clerkId);
            var chatRoomsDto = chats.Select(m => new ChatRoomDto(m)).ToList();
            return Result<List<ChatRoomDto>>.Ok(chatRoomsDto);
        }

        public async Task<Result<List<ChatMessageDto>>> MarkMessageAsSeenAsync(Guid chatRoomId, string viewerClerkId)
        {
            try
            {
                var result = await _chatRepository.MarkMessageAsSeenAsync(chatRoomId, viewerClerkId);
                var messages = result.Select(m => new ChatMessageDto(m)).ToList();
                return Result<List<ChatMessageDto>>.Ok(messages);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Result<ChatMessageDto>> MarkMessageAsDeliveredAsync(Guid messageId, string recipientClerkId)
        {
            try
            {
                var deliveredMessage = await _chatRepository.MarkMessageAsDeliveredAsync(messageId, recipientClerkId);
                if (deliveredMessage != null)
                {
                    var messageDto = new ChatMessageDto(deliveredMessage);
                    return Result<ChatMessageDto>.Ok(messageDto);
                }
                return Result<ChatMessageDto>.Fail("Message not found or already delivered.");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ChatMessageDto>> GetUndeliveredMessagesAsync(Guid roomId, string recipientId)
        {
            try
            {
                var chatsMessages = await _chatRepository.GetUndeliveredMessagesAsync(roomId, recipientId);

                if (chatsMessages != null)
                {
                    var chatMessagesDto = chatsMessages.Select(m => new ChatMessageDto(m)).ToList();
                    return chatMessagesDto;
                }
                return new List<ChatMessageDto>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
