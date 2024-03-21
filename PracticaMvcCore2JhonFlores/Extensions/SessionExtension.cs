using Newtonsoft.Json;

namespace PracticaMvcCore2JhonFlores.Extensions
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, string Key, object value)
        {
            string data = JsonConvert.SerializeObject(value);
            session.SetString(Key, data);
        }

        public static T GetObject<T>(this ISession session, string Key)
        {
            string data = session.GetString(Key);
            if (data == null)
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}
