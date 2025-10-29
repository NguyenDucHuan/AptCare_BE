﻿using AptCare.Service.Dtos.ApproveReportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptCare.Service.Services.Interfaces
{
    public interface IReportApprovalService
    {
        Task<string> ApproveReportAsync(ApproveReportCreateDto dto);

    }
}
