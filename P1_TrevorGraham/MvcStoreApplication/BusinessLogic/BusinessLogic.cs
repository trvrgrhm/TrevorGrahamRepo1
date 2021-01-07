using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BusinessLogic
    {
        private readonly Repository _repository;
        private readonly Mapper _mapper;
        public BusinessLogic(Repository repository, Mapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
