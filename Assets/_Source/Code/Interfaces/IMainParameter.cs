using _Source.Code._AKFramework.AKTags.Runtime;

namespace _Source.Code.Interfaces
{
    public interface IMainParameter<out T>
    {
        AKTag GetParameterTag();
        T GetMainParameterValue();
    }
}