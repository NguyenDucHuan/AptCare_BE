﻿using AptCare.Repository.Entities;
using AptCare.Repository.Enum;
using AptCare.Repository.Enum.Apartment;
using AptCare.Service.Dtos.Account;
using AptCare.Service.Dtos.BuildingDtos;
using AptCare.Service.Dtos.ChatDtos;
using AptCare.Service.Dtos.RepairRequestDtos;
using AptCare.Service.Dtos.IssueDto;
using AptCare.Service.Dtos.TechniqueDto;
using AptCare.Service.Dtos.UserDtos;
using AptCare.Service.Dtos.WorkSlotDtos;
using AutoMapper;
using AptCare.Service.Dtos.AppointmentDtos;
using AptCare.Service.Dtos;
using AptCare.Service.Dtos.SlotDtos;
using AptCare.Service.Dtos.InvoiceDtos;
using System.ComponentModel.DataAnnotations.Schema;
using AptCare.Service.Dtos.InspectionReporDtos;
using AptCare.Repository.Enum.AccountUserEnum;
using AptCare.Service.Dtos.NotificationDtos;

namespace AptCare.Api.MapperProfile
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // ===== Account =====
            CreateMap<Account, AccountForAdminDto>()
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Role.ToString()))
                .ForMember(d => d.LockoutEnabled, o => o.MapFrom(s => s.LockoutEnabled))
                .ForMember(d => d.LockoutEnd, o => o.MapFrom(s => s.LockoutEnd));
            CreateMap<Account, AccountDto>()
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Role.ToString()));

            // ===== UserApartment -> ApartmentForUser* (map con, dùng lại) =====
            CreateMap<UserApartment, ApartmentForUserDto>()
                .ForMember(d => d.Room, o => o.MapFrom(s => s.Apartment.Room))
                .ForMember(d => d.RoleInApartment, o => o.MapFrom(s => s.RoleInApartment.ToString()))
                .ForMember(d => d.RelationshipToOwner, o => o.MapFrom(s => s.RelationshipToOwner));

            CreateMap<UserApartment, ApartmentForUserProfileDto>()
                .ForMember(d => d.Room, o => o.MapFrom(s => s.Apartment.Room))
                .ForMember(d => d.RoleInApartment, o => o.MapFrom(s => s.RoleInApartment.ToString()))
                .ForMember(d => d.RelationshipToOwner, o => o.MapFrom(s => s.RelationshipToOwner))
                .ForMember(d => d.Floor, o => o.MapFrom(s => s.Apartment.Floor.FloorNumber));

            // ===== User -> UserDto / GetOwnProfileDto =====
            CreateMap<User, UserDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Apartments, o => o.MapFrom(s => s.UserApartments))
                .ForMember(d => d.AccountInfo, o => o.MapFrom(s => s.Account));

            CreateMap<User, UserGetAllDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Apartments, o => o.MapFrom(o => (List<ApartmentForUserDto>?)null))
                .ForMember(d => d.AccountInfo, o => o.MapFrom(o => (AccountForAdminDto?)null))
                .ForMember(d => d.IshaveAccount, o => o.MapFrom(s => s.Account != null))
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Account != null ? s.Account.Role.ToString() : "NULL"));

            CreateMap<User, UserBasicDto>();

            CreateMap<User, GetOwnProfileDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Account.Role.ToString()))
                .ForMember(d => d.Apartments, o => o.MapFrom(s => s.UserApartments))
                .ForMember(d => d.Techniques, o => o.MapFrom(s => s.TechnicianTechniques));

            // Cho tạo/cập nhật User
            CreateMap<CreateUserDto, User>();

            CreateMap<UpdateUserDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ===== Floor =====
            CreateMap<Floor, FloorDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));
            CreateMap<Floor, GetAllFloorsDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.ApartmentCount, o => o.MapFrom(s => s.Apartments.Count))
                .ForMember(d => d.CommonAreaCount, o => o.MapFrom(s => s.CommonAreas.Count))
                .ForMember(d => d.ResidentCount, o => o.MapFrom(s => s.Apartments.Sum(a => a.UserApartments.Count)))
                .ForMember(d => d.Apartments, o => o.Ignore())
                .ForMember(d => d.CommonAreas, o => o.Ignore());
            CreateMap<FloorCreateDto, Floor>()
                .ForMember(d => d.Status, o => o.MapFrom(s => ActiveStatus.Active));
            CreateMap<FloorUpdateDto, Floor>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ===== Apartment =====
            CreateMap<Apartment, ApartmentDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Floor, o => o.MapFrom(s => s.Floor.FloorNumber.ToString()))
                .ForMember(d => d.Users, o => o.MapFrom(s => s.UserApartments));

            CreateMap<UserApartment, UserInApartmentDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.User, o => o.MapFrom(s => new UserDto
                {
                    UserId = s.User.UserId,
                    FirstName = s.User.FirstName,
                    LastName = s.User.LastName,
                    Email = s.User.Email,
                    PhoneNumber = s.User.PhoneNumber,
                    CitizenshipIdentity = s.User.CitizenshipIdentity,
                    Birthday = s.User.Birthday,
                    Apartments = null, // tránh vòng lặp
                    Status = s.User.Status.ToString(),
                    AccountInfo = s.User.Account == null ? null : new AccountForAdminDto
                    {
                        AccountId = s.User.Account.AccountId,
                        Username = s.User.Account.Username,
                        Role = s.User.Account.Role.ToString(),
                        EmailConfirmed = s.User.Account.EmailConfirmed,
                        LockoutEnabled = s.User.Account.LockoutEnabled,
                        LockoutEnd = s.User.Account.LockoutEnd
                    }
                }));

            CreateMap<ApartmentCreateDto, Apartment>()
                .ForMember(d => d.Status, o => o.MapFrom(s => ApartmentStatus.Active));
            CreateMap<ApartmentUpdateDto, Apartment>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateApartmentWithResidentDataDto, Apartment>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ResidentOfApartmentDto, UserApartment>()
                .ForMember(d => d.Status, o => o.MapFrom(s => ActiveStatus.Active))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow.AddHours(7)))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ===== CommonArea =====
            CreateMap<CommonArea, CommonAreaDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Floor, o => o.MapFrom(s => s.Floor.FloorNumber.ToString()));
            CreateMap<CommonAreaCreateDto, CommonArea>()
                .ForMember(d => d.Status, o => o.MapFrom(s => ActiveStatus.Active));
            CreateMap<CommonAreaUpdateDto, CommonArea>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ===== WorkSlot =====
            CreateMap<WorkSlotUpdateDto, WorkSlot>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<WorkSlot, TechnicianWorkSlotDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            // ===== Message / Chat =====
            CreateMap<TextMessageCreateDto, Message>()
                .ForMember(d => d.Status, o => o.MapFrom(s => MessageStatus.Sent))
                .ForMember(d => d.Type, o => o.MapFrom(s => MessageType.Text))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow.AddHours(7)));

            CreateMap<Message, MessageDto>()
                .ForMember(d => d.Slug, o => o.MapFrom(s => s.Conversation.Slug))
                .ForMember(d => d.SenderName, o => o.MapFrom(s => s.Sender.FirstName + " " + s.Sender.LastName))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.ToString()))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.ReplyType, o => o.MapFrom(s => s.ReplyMessage != null ? s.ReplyMessage.Type.ToString() : null))
                .ForMember(d => d.ReplyContent, o => o.MapFrom(s => s.ReplyMessage != null ? s.ReplyMessage.Content : null))
                .ForMember(d => d.ReplySenderName, o => o.MapFrom(s => s.ReplyMessage != null
                                                        ? s.ReplyMessage.Sender.FirstName + " " + s.ReplyMessage.Sender.LastName
                                                        : null))
                .ForMember(d => d.IsMine, o => o.Ignore());

            CreateMap<Account, AccountForAdminDto>()
               .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

            //REPAIR REQUEST
            CreateMap<RepairRequest, RepairRequestDto>()
                .ForMember(d => d.ChildRequestIds, o => o.MapFrom(s => s.ChildRequests != null ? s.ChildRequests.Select(x => x.RepairRequestId) : null))
                .ForMember(d => d.RepairReportId, o => o.MapFrom(s =>
                    s.Appointments != null
                        ? s.Appointments
                            .Where(a => a.RepairReport != null)
                            .Select(a => a.RepairReport)
                            .FirstOrDefault() != null
                                ? s.Appointments
                                    .Where(a => a.RepairReport != null)
                                    .Select(a => a.RepairReport.RepairReportId)
                                    .FirstOrDefault()
                                : (int?)null
                        : (int?)null
                ))
                .ForMember(d => d.InspectionReportIds, o => o.MapFrom(s =>
                    s.Appointments != null
                        ? s.Appointments
                            .Where(a => a.InspectionReports != null && a.InspectionReports.Count > 0)
                            .SelectMany(a => a.InspectionReports)
                            .Select(ir => ir.InspectionReportId)
                            .ToList()
                        : null
                ));

            CreateMap<RepairRequest, RepairRequestBasicDto>();
            CreateMap<RequestTracking, RequestTrackingDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            //APOINTMENT
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.AppointmentTrackings.LastOrDefault().Status.ToString()))
                .ForMember(d => d.Technicians, o => o.MapFrom(s => s.AppointmentAssigns.Select(x => x.Technician)));
            CreateMap<AppointmentCreateDto, Appointment>()
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow.AddHours(7)));
            CreateMap<AppointmentUpdateDto, Appointment>();

            CreateMap<RepairRequestNormalCreateDto, RepairRequest>()
               .ForMember(dest => dest.IsEmergency, opt => opt.MapFrom(src => false))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow.AddHours(7)));
            CreateMap<RepairRequestEmergencyCreateDto, RepairRequest>()
               .ForMember(dest => dest.IsEmergency, opt => opt.MapFrom(src => true))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow.AddHours(7)));

            // ===== Issue =====
            CreateMap<IssueCreateDto, Issue>()
                .ForMember(d => d.Status, o => o.MapFrom(s => ActiveStatus.Active));
            CreateMap<Issue, IssueListItemDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.TechniqueName, o => o.MapFrom(s => s.Technique.Name));
            CreateMap<IssueUpdateDto, Issue>()
                .ForMember(d => d.Status, o => o.MapFrom(s => Enum.Parse<ActiveStatus>(s.Status)));
            CreateMap<Technique, TechniqueListItemDto>()
                .ForMember(d => d.IssueCount, o => o.MapFrom(s => s.Issues.Count));
            CreateMap<TechniqueCreateDto, Technique>();
            CreateMap<TechniqueUpdateDto, Technique>();
            CreateMap<TechnicianTechnique, TechniqueResponseDto>()
                .ForMember(e => e.TechniqueName, o => o.MapFrom(s => s.Technique.Name))
                .ForMember(e => e.Description, o => o.MapFrom(s => s.Technique.Description));

            //MEDIA
            CreateMap<Media, MediaDto>();

            //SLOT
            CreateMap<Slot, SlotDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));
            CreateMap<SlotCreateDto, Slot>()
                .ForMember(d => d.Status, o => o.MapFrom(s => ActiveStatus.Active))
                .ForMember(d => d.LastUpdated, o => o.MapFrom(s => DateTime.UtcNow.AddHours(7)));
            CreateMap<SlotUpdateDto, Slot>()
                .ForMember(d => d.LastUpdated, o => o.MapFrom(s => DateTime.UtcNow.AddHours(7)));

            //INVOICE
            CreateMap<Invoice, InvoiceDto>()
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.ToString()))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.Accessories, o => o.MapFrom(s => s.InvoiceAccessories))
            .ForMember(d => d.Services, o => o.MapFrom(s => s.InvoiceServices));
            CreateMap<InvoiceAccessory, InvoiceAccessoryDto>();
            CreateMap<InvoiceService, InvoiceServiceDto>();

            CreateMap<InvoiceInternalCreateDto, Invoice>()
                .ForMember(d => d.Status, o => o.MapFrom(s => ActiveStatus.Active))
                .ForMember(d => d.Type, o => o.MapFrom(s => InvoiceType.InternalRepair))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow.AddHours(7)))
            .ForMember(d => d.InvoiceAccessories, o => o.MapFrom(s => new List<InvoiceAccessory>()))
            .ForMember(d => d.InvoiceServices, o => o.MapFrom(s => new List<InvoiceService>()));
            CreateMap<InvoiceExternalCreateDto, Invoice>()
                .ForMember(d => d.Status, o => o.MapFrom(s => ActiveStatus.Active))
                .ForMember(d => d.Type, o => o.MapFrom(s => InvoiceType.ExternalContractor))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow.AddHours(7)))
                .ForMember(d => d.InvoiceAccessories, o => o.MapFrom(s => new List<InvoiceAccessory>()))
                .ForMember(d => d.InvoiceServices, o => o.MapFrom(s => new List<InvoiceService>()));
            CreateMap<InvoiceUpdateDto, Invoice>();


            //InspectionReport
            CreateMap<InspectionReport, InspectionReportDto>()
                .ForMember(d => d.Technican, o => o.MapFrom(s => s.User))
                .ForMember(d => d.AreaName, o => o.MapFrom(s => s.Appointment.RepairRequest.MaintenanceRequest != null
                    ? s.Appointment.RepairRequest.MaintenanceRequest.CommonAreaObject.Name
                    : s.Appointment.RepairRequest.Apartment != null
                        ? s.Appointment.RepairRequest.Apartment.Room
                        : string.Empty));
            CreateMap<InspectionReport, InspectionBasicReportDto>()
                .ForMember(d => d.Technican, o => o.MapFrom(s => s.User))
                .ForMember(d => d.AreaName, o => o.MapFrom(s => s.Appointment.RepairRequest.MaintenanceRequest != null
                    ? s.Appointment.RepairRequest.MaintenanceRequest.CommonAreaObject.Name
                    : s.Appointment.RepairRequest.Apartment != null
                        ? s.Appointment.RepairRequest.Apartment.Room
                        : string.Empty));

            CreateMap<CreateInspectionReporDto, InspectionReport>()
                .ForMember(d => d.Status, o => o.MapFrom(s => ReportStatus.Pending))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow.AddHours(7)));

            CreateMap<UpdateInspectionReporDto, InspectionReport>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<User, TechnicanDto>()
                .ForMember(d => d.workStatus, o => o.Ignore())
                .ForMember(d => d.Techniques, o => o.MapFrom(s => s.TechnicianTechniques.Select(tt => tt.Technique.Name)))
                .AfterMap((src, dest) =>
                {
                    var todayWorkSlot = src.WorkSlots != null
                        ? src.WorkSlots.FirstOrDefault(ws => ws.Date == DateOnly.FromDateTime(DateTime.UtcNow.AddHours(7)))
                        : null;
                    dest.workStatus = todayWorkSlot?.Status ?? WorkSlotStatus.Off;
                });

            //NOTIFICATION
            CreateMap<NotificationCreateDto, NotificationPushRequestDto>();
            CreateMap<NotificationPushRequestDto, Notification>()
                .ForMember(d => d.IsRead, o => o.MapFrom(s => false))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow.AddHours(7)));
            CreateMap<Notification, NotificationDto>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.ToString()));
        }
    }
}
