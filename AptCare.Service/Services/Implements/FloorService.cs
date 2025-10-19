﻿using AptCare.Repository;
using AptCare.Repository.Entities;
using AptCare.Repository.Paginate;
using AptCare.Repository.UnitOfWork;
using AptCare.Service.Dtos;
using AptCare.Service.Dtos.BuildingDtos;
using AptCare.Service.Exceptions;
using AptCare.Service.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AptCare.Service.Services.Implements
{
    public class FloorService : BaseService<FloorService>, IFloorService
    {
        public FloorService(IUnitOfWork<AptCareSystemDBContext> unitOfWork, ILogger<FloorService> logger, IMapper mapper) : base(unitOfWork, logger, mapper)
        {
        }

        public async Task<string> CreateFloorAsync(FloorCreateDto dto)
        {
            try
            {
                var isDupFloor = await _unitOfWork.GetRepository<Floor>().AnyAsync(
                    predicate: x => x.FloorNumber == dto.FloorNumber
                    );

                if (isDupFloor)
                {
                    throw new AppValidationException("Số tầng đã tồn tại.");
                }

                var floor = _mapper.Map<Floor>(dto);

                await _unitOfWork.GetRepository<Floor>().InsertAsync(floor);
                await _unitOfWork.CommitAsync();
                return "Taọ tầng mới thành công";
            }
            catch (Exception e)
            {
                throw new AppValidationException($"Lỗi hệ thống: {e.Message}", StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<string> UpdateFloorAsync(int id, FloorUpdateDto dto)
        {
            try
            {
                var floor = await _unitOfWork.GetRepository<Floor>().SingleOrDefaultAsync(
                    predicate: x => x.FloorId == id
                    );

                if (floor == null)
                {
                    throw new AppValidationException("Tầng không tồn tại.", StatusCodes.Status404NotFound);
                }

                var isDupFloor = await _unitOfWork.GetRepository<Floor>().AnyAsync(
                    predicate: x => x.FloorNumber == dto.FloorNumber
                    );

                if (isDupFloor)
                {
                    throw new AppValidationException("Số tầng đã tồn tại.");
                }


                _mapper.Map(dto, floor);
                _unitOfWork.GetRepository<Floor>().UpdateAsync(floor);
                await _unitOfWork.CommitAsync();
                return "Cập nhật tầng thành công";
            }
            catch (Exception e)
            {
                throw new AppValidationException($"Lỗi hệ thống: {e.Message}", StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<string> DeleteFloorAsync(int id)
        {
            try
            {
                var floor = await _unitOfWork.GetRepository<Floor>().SingleOrDefaultAsync(
                    predicate: x => x.FloorId == id
                    );

                if (floor == null)
                {
                    throw new AppValidationException("Tầng không tồn tại.", StatusCodes.Status404NotFound);
                }

                _unitOfWork.GetRepository<Floor>().DeleteAsync(floor);
                await _unitOfWork.CommitAsync();
                return "Xóa tầng thành công";
            }
            catch (Exception e)
            {
                throw new AppValidationException($"Lỗi hệ thống: {e.Message}", StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<FloorDto> GetFloorByIdAsync(int id)
        {
            var floor = await _unitOfWork.GetRepository<Floor>().SingleOrDefaultAsync(
                selector: x => _mapper.Map<FloorDto>(x),
                predicate: p => p.FloorId == id,
                include: i => i.Include(x => x.Apartments)
                               .Include(x => x.CommonAreas)
                );

            if (floor == null)
            {
                throw new AppValidationException("Tầng không tồn tại", StatusCodes.Status404NotFound);
            }

            return floor;
        }

        public async Task<IPaginate<FloorDto>> GetPaginateFloorAsync(PaginateDto dto)
        {
            int page = dto.page > 0 ? dto.page : 1;
            int size = dto.size > 0 ? dto.size : 10;
            string search = dto.search?.ToLower() ?? string.Empty;
            string filter = dto.filter?.ToLower() ?? string.Empty;

            Expression<Func<Floor, bool>> predicate = p =>
                (string.IsNullOrEmpty(search) || p.FloorNumber.ToString().Contains(search) ||
                                                 p.Description.Contains(search)) &&
                (string.IsNullOrEmpty(filter) ||
                filter.Equals(p.Status.ToString().ToLower()));

            var result = await _unitOfWork.GetRepository<Floor>().GetPagingListAsync(
                selector: x => _mapper.Map<FloorDto>(x),
                predicate: predicate,
                include: i => i.Include(x => x.Apartments)
                               .Include(x => x.CommonAreas),
                orderBy: BuildOrderBy(dto.sortBy),
                    page: page,
                    size: size
                );

            return result;
        }

        private Func<IQueryable<Floor>, IOrderedQueryable<Floor>> BuildOrderBy(string sortBy)
        {
            if (string.IsNullOrEmpty(sortBy)) return q => q.OrderByDescending(p => p.FloorId);

            return sortBy.ToLower() switch
            {
                "floor" => q => q.OrderBy(p => p.FloorNumber),
                "floor_desc" => q => q.OrderByDescending(p => p.FloorNumber),
                _ => q => q.OrderByDescending(p => p.FloorNumber) // Default sort
            };
        }
    }
}
