using System.Threading.Tasks;
using Grpc.Core;
using OzonEdu.MerchandiseService.Grpc;
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
        public override Task<ItemMerch> RequestMerch(RequestMerchItem request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unknown, "Нет реализации"),
                new Metadata {new Metadata.Entry("key", "our value")});
        }
        public override Task<ItemMerch> ResponseMerch(Employee id, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unknown, "Нет реализации"),
                new Metadata {new Metadata.Entry("key", "our value")});
        }
    }
}