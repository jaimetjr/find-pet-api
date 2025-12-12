using Application.DTOs.Notification;
using Application.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;
    public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<NotificationDto>>> GetNotificationsPaged(string clerkId, int? page = 0, int? limit = 0)
    {
        var notifications = await _notificationRepository.GetNotificationsPaged(clerkId, page, limit);

        return Result<IEnumerable<NotificationDto>>.Ok(_mapper.Map<IEnumerable<NotificationDto>>(notifications));
    }

    public async Task<Result<IEnumerable<NotificationDto>>> GetUnreadNotifications(string clerkId)
    {
        var notifications = await _notificationRepository.GetUnreadNotifications(clerkId);
        return Result<IEnumerable<NotificationDto>>.Ok(_mapper.Map<IEnumerable<NotificationDto>>(notifications));
    }

    public async Task<Result<string>> MarkNotificationAsRead(string clerkId, Guid id)
    {
        await _notificationRepository.MarkNotificationAsRead(clerkId, id);
        return Result<string>.Ok("Notification marked as read.");
    }

    public async Task<Result<string>> MarkAllNotificationAsRead(string clerkId)
    {
        await _notificationRepository.MarkAllNotificationAsRead(clerkId);
        return Result<string>.Ok("All Notification marked as read.");
    }
}
