using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Configuration
{
    public class Conf
    {
        private int _errors = 0;
        private Inter _inter;

        public Conf()
        {
            _errors += LoadInter("DEV");
        }

        public bool Ok
        {
            get => _errors == 0;
        }

        public Inter Inter
        {
            get => _inter;
        }

        public int LoadInter(string conf)
        {
            TextReader tr = new StringReader(File.ReadAllText(Directory.GetCurrentDirectory() + @"/Conf/Inter.yml", Encoding.ASCII));
            var deserializer = new DeserializerBuilder().Build();
            var parser = new Parser(tr);
            parser.Consume<StreamStart>();

            while (parser.Accept<DocumentStart>())
            {
                var document = deserializer.Deserialize<InterData>(parser);

                foreach (var i in document.InterList)
                {
                    if (!i.Configuracao.Equals(conf))
                        continue;

                    _inter = i;
                }
            }

            if (_inter == null)
            {
                return 1;
            }

            return 0;
        }
    }
}
