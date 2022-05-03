using System;
using System.Collections.Generic;
using System.Text;

namespace Pra.Streams02.Core
{
    public class EncodingEntity
    {
        public Encoding CharacterSet { get; }
        public string CharacterSetName { get; }
        public EncodingEntity(Encoding characterSet, string characterSetName)
        {
            CharacterSet = characterSet;
            CharacterSetName = characterSetName;
        }
        public override string ToString()
        {
            return CharacterSetName;
        }
    }

    public class EncodingService
    {
        public List<EncodingEntity> AvailableCharSets { get; set; }
        public EncodingService()
        {
            AvailableCharSets = new List<EncodingEntity>();
            AvailableCharSets.Add(new EncodingEntity(Encoding.UTF8, "UTF-8"));
            AvailableCharSets.Add(new EncodingEntity(Encoding.GetEncoding("iso-8859-1"), "Ansi"));
        }
    }
}
