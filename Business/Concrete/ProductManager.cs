using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performans;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        /// <summary>
        /// bir manager içerisinde kendisinden başka dal eklenemez!...
        /// </summary>
        /// <param name="productDal"></param>
        /// <param name="categoryService"></param>
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        //Kampın Orjinal Metodu
        [CacheAspect] //key=cache ismi Value=değeri.
        public IDataResult<IList<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<IList<Product>>(Messages.MaintenanceTime);
            }

            var productList = _productDal.GetAll();

            //return new DataResult<List<Product>>(_productDal.GetAll(),true,Messages.ProductListed);
            return new SuccessDataResult<IList<Product>>(productList, Messages.ProductListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>((List<Product>)_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            List<Product> products = _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max).ToList();
            return new SuccessDataResult<List<Product>>(products);
        }

        public IDataResult<IList<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour != 22)
            {
                return new ErrorDataResult<IList<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<IList<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        //Kampın ORGINAL Metodu
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult AddProduct(Product product)
        {
            //iş kurallarını çalıştıracak.dönüş null ise/tüm kurallara uyuyorsa dönüş null dır.
            IResult result = BusinessRules.Run(PruductCountControl(product.CategoryId), ProductNameControl(product.ProductName));

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult UpdateProduct(Product product)
        {
            if (!PruductCountControl(product.CategoryId).Success)
            {
                return new ErrorResult(Messages.CategoryCountFailed);
            }

            //business code
            _productDal.Update(product);

            return new Result(true, Messages.ProductAdded);
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            AddProduct(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }

            AddProduct(product);
            return null;
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Product> GetByProductId(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id));
        }

        /// <summary>
        /// bir kategoriden 10 dan fazla ürün eklenemez.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private IResult PruductCountControl(int categoryId)
        {
            var count = _productDal.GetAll(x => x.CategoryId == categoryId).Count;
            if (count >= 10)
            {
                return new ErrorResult(Messages.CategoryCountFailed);
            }

            return new SuccessResult();
        }

        /// <summary>
        /// Bir isimden sadece bir kez kullanılabilir.
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        private IResult ProductNameControl(string productName)
        {
            bool countName = _productDal.GetAll(x => x.ProductName == productName).Any();
            if (countName)
            {
                return new ErrorResult(Messages.AlreadyProductNameExist);
            }
            return new SuccessResult();
        }

        
    }
}
