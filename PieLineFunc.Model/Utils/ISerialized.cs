namespace PieLineFunc.Model.Utils
{
    public interface ISerialized
    {
        void Serialized<T>(T @object, string path);
        T Deserialized<T>(string path);
    }
}