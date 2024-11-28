namespace academia.WebApi.Controllers.Base
{
    public interface IResponseFactory
    {
        IResponse CreateResponse(string httpMethods);

    }
}
