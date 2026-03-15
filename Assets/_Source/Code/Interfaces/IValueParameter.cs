using _Source.Code._AKFramework.AKTags.Runtime;

namespace _Source.Code.Interfaces
{
    public interface IValueParameter<out T> where T: struct // u can do this with SO
    {
    AKTag GetParameterTag();
    T GetValueParameterValue();
    }
}