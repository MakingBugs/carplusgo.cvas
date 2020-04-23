using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarPlusGo.CVAS.CarFixFile.Dto;
using Microsoft.EntityFrameworkCore;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;

namespace CarPlusGo.CVAS.CarFixFile
{
    public class CarFixAppService
        : AsyncCrudAppService<CarFix, CarFixDto, long, CarFixResultRequestDto, CarFixDto, CarFixDto>, ICarFixAppService
    {

        private readonly IRepository<CarBase, long> _carbaseRepository;
        private readonly IRepository<CarFixItem, long> _carfixitemRepository;
        private readonly IRepository<Supplier, long> _supplierRepository;
        private readonly IRepository<ItemCode, long> _itemcodeRepository;
        private readonly IRepository<PRInvLink, long> _prinvlinkRepository;
        private readonly IRepository<CarFixBatch, long> _carfixbatchRepository;
        private readonly IRepository<AccBank, long> _accbankRepository;

        public CarFixAppService(IRepository<CarFix, long> repository,
            IRepository<CarBase, long> carbaseRepository,
            IRepository<CarFixItem, long> carfixitemRepository,
            IRepository<Supplier, long> supplierRepository,
            IRepository<ItemCode, long> itemcodeRepository,
            IRepository<PRInvLink, long> prinvlinkRepository,
            IRepository<CarFixBatch, long> carfixbatchRepository,
            IRepository<AccBank, long> accbankRepository)
           : base(repository)
        {
            _carbaseRepository = carbaseRepository;
            _carfixitemRepository = carfixitemRepository;
            _supplierRepository = supplierRepository;
            _itemcodeRepository = itemcodeRepository;
            _prinvlinkRepository = prinvlinkRepository;
            _carfixbatchRepository = carfixbatchRepository;
            _accbankRepository = accbankRepository;
        }
        protected override IQueryable<CarFix> CreateFilteredQuery(CarFixResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.CarBase, x => x.CarFixItem, x => x.Supplier, x => x.ItemCodeFixTypeData, x => x.ItemCodeStatusData, x => x.PRInvLink,x=>x.CarFixBatch,x=>x.AccBank)
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Status != -10)
                .WhereIf(input.Id.HasValue, x => x.Id == input.Id)
                .WhereIf(input.CarBaseAuto.HasValue, x => x.CarBaseAuto == input.CarBaseAuto)
                .WhereIf(input.MakNo != null && input.MakNo != "", x => x.MakNo.Contains(input.MakNo))
                .WhereIf(input.Cdt.HasValue,x=>x.Cdt.Date==input.Cdt)
                .WhereIf(input.SupplierAuto.HasValue,x=>x.SupplierAuto==input.SupplierAuto)
                .WhereIf(input.CarFixBatchAuto.HasValue,x=>x.CarFixBatchAuto==input.CarFixBatchAuto)
                .WhereIf(input.Status.Count()>0,x=>input.Status.Any(s=>x.Status==s));


            //var list = from carfix in Repository.GetAllIncluding(x=>x.CarBase,x=>x.PRInvLink,x=>x.Supplier)
            //           join carfixitem in _carfixitemRepository.GetAll()
            //           on carfix.Id equals carfixitem.CarFixAuto into cfcfi
            //           from cfcfii in cfcfi.DefaultIfEmpty()
            //join supplier in _supplierRepository.GetAll()
            //on carfix.SupplierAuto equals supplier.Id into cfs
            //from cfsi in cfs.DefaultIfEmpty()
            //join itemcodeftd in _itemcodeRepository.GetAll()
            //           on new { ItemType = carfix.ItemCodeFixType, Num = carfix.FixType } equals new { ItemType = itemcodeftd.ItemType, Num = itemcodeftd.Num }
            //           into cficftd
            //           from cficftdi in cficftd.DefaultIfEmpty()
            //           join itemcodes in _itemcodeRepository.GetAll()
            //           on new { ItemType = carfix.ItemCodeStatus, Num = carfix.Status } equals new { ItemType = itemcodes.ItemType, Num = itemcodes.Num }
            //           into cfics
            //           from cficsi in cfics.DefaultIfEmpty()
            //           join carfixbatch in _carfixbatchRepository.GetAll()
            //           on carfix.CarFixBatchAuto equals carfixbatch.Id into cfcfb
            //           from cfcfbi in cfcfb.DefaultIfEmpty()
            //           join accbank in _accbankRepository.GetAll()
            //           on carfix.AccBankAuto equals accbank.Id into cfab
            //           from cfabi in cfab.DefaultIfEmpty()
            //           select new CarFix
            //           {
            //               Id = carfix.Id,
            //               CarBaseAuto = carfix.CarBaseAuto,
            //               OrderAuto = carfix.OrderAuto,
            //               SupplierAuto = carfix.SupplierAuto,
            //               //Supplier = cfsi,
            //               CarFixNo = carfix.CarFixNo,
            //               MakNo = carfix.MakNo,
            //               CustName = carfix.CustName,
            //               FixDt = carfix.FixDt,
            //               FixDtpre = carfix.FixDtpre,
            //               FixDtreal = carfix.FixDtreal,
            //               Km = carfix.Km,
            //               FixType = carfix.FixType,
            //               ItemCodeFixType = carfix.ItemCodeFixType,
            //               ItemCodeFixTypeData = carfix.ItemCodeFixTypeData

            //           };
            //return list.Where(x => x.IsDeleted == false)
            //    .Where(x => x.Status != -10)
            //    .WhereIf(input.Id.HasValue, x => x.Id == input.Id)
            //    .WhereIf(input.CarBaseAuto.HasValue, x => x.CarBaseAuto == input.CarBaseAuto)
            //    .WhereIf(input.MakNo != null && input.MakNo != "", x => x.MakNo.Contains(input.MakNo))
            //    .WhereIf(input.Status.HasValue, x => x.Status == input.Status);
        }
    }
}
