using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// PastForm - content of words past form (if appropriate).
    /// PastThForm - content of words past participle (if appropriate).
    /// PluralForm - content of words plural form (if appropriate).
    /// </summary>
    public class WordForm : EntityTable
    {
        [DataMember]
        public string PastForm { get; set; } = null;
        [DataMember]
        public string PastThForm { get; set; } = null;
        [DataMember]
        public string PluralForm { get; set; } = null;
    }
}