using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OzonEdu.MerchandiseService.Grpc;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services.Interfaces;
namespace OzonEdu.MerchandiseService.GrpcServices
{
    public class MerchGrpcService : MerchServiceGrpc.MerchServiceGrpcBase
    {
        private readonly IMerchService _merchService;

        public MerchGrpcService(IMerchService merchService)
        {
            _merchService = merchService;
        }
        
    }
}