namespace PraktikaApplication.Static
{
    public static class ContactsToDict
    {
        public static string DictToString(Dictionary<string, string> dict)
        {
            return string.Join(";", dict.Select(x => $"{x.Key}:{x.Value}"));
        }
        public static Dictionary<string, string> StringToDict(string str)
        {
            var dict = new Dictionary<string, string>();
            foreach (var sub in str.Split(";"))
            {
                var KeyValue = sub.Split(":");
                dict[KeyValue[0]] = KeyValue[1];
            }
            return dict;
        }
    }
}
