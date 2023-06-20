using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ModelManager : IModelService
    {
        private readonly IModelDal _modelDal;

        public ModelManager(IModelDal modelDal)
        {
            _modelDal = modelDal;
        }

        public IResult Add(Model model)
        {
            _modelDal.Add(model);

            return new SuccessResult();
        }

        public IResult Delete(Model model)
        {
            _modelDal.Delete(model);

            return new SuccessResult();
        }

        public IDataResult<List<Model>> GetAll()
        {
            return new SuccessDataResult<List<Model>>(_modelDal.GetAll());
        }

        public IDataResult<List<Model>> GetAllByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Model>>(
                _modelDal.GetAll(model => model.BrandId == brandId)
            );
        }

        public IDataResult<Model> GetById(int id)
        {
            return new SuccessDataResult<Model>(_modelDal.Get(model => model.Id == id));
        }

        public IResult Update(Model model)
        {
            _modelDal.Update(model);

            return new SuccessResult();
        }
    }
}
