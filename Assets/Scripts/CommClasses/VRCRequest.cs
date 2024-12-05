using System.Text.Json.Serialization;
using System.Threading.Tasks;

[JsonDerivedType(typeof(RequestVRCControl), typeDiscriminator: 0)]
[JsonDerivedType(typeof(RequestVRCKnob), typeDiscriminator: 1)]
[JsonDerivedType(typeof(RequestVRCToggle), typeDiscriminator: 2)]
[JsonDerivedType(typeof(RequestVRCButton), typeDiscriminator: 3)]
public abstract class VRCRequest
{
    public abstract Task HandleRequest();
}