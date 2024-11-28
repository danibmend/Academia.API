
using academia.WebApi.Controllers.Base.Response;

namespace academia.WebApi.Controllers.Base
{
    public class ResponseFactory: IResponseFactory
    {
        
        public IResponse CreateResponse(string httpMethod)
        {
            if (httpMethod == HttpMethods.Get)
            {
                return new GetResponse();
            }

            if (httpMethod == HttpMethods.Put)
            {
                return new PutResponse();
            }

            if (httpMethod == HttpMethods.Patch)
            {
                return new PatchReponse();
            }

            if (httpMethod == HttpMethods.Post)
            {
                return new PostResponse();
            }

            if (httpMethod == HttpMethods.Delete)
            {
                return new DeleteResponse();
            }

            return null;
        }
    }
}
