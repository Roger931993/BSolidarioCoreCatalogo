using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Core.Catalog.Infrastructure.ExternalServices.Common.Wcf
{
  public class CustomerHeaderEndpointBehavior: IEndpointBehavior
  {
    private readonly IDictionary<string, string> _header;

    public CustomerHeaderEndpointBehavior(IDictionary<string,string> Header)
    {
      _header = Header;
    }
    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameter)
    {

    }
    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    {
      clientRuntime.ClientMessageInspectors.Add(new CustomerHeaderMeesageInspector(_header));
    }

    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
    {     
    }

    public void Validate(ServiceEndpoint endpoint)
    {     
    }
  }

  public class CustomerHeaderMeesageInspector : IClientMessageInspector
  {
    private readonly IDictionary<string, string> _header;

    public CustomerHeaderMeesageInspector(IDictionary<string,string> header)
    {
      this._header = header;
    }

    public void AfterReceiveReply(ref Message reply, object correlationState)
    {     
    }

    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    {
      if (_header!= null)
      {
        foreach (KeyValuePair<string, string> item in _header)
        {
          request.Headers.Add(MessageHeader.CreateHeader(item.Key, "http://temp.uri", item.Value));
        }
      }
      return null;
    }
  }
}
