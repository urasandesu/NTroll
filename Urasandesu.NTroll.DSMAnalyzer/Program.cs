using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using System.Xml;
using Urasandesu.NAnonym;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"NewFile.xml";
            var modules = GetModules(path);
            var idModuleDictionary = new Dictionary<string, Module>();
            var parentSet = new HashSet<string>();
            foreach (var module in modules.Where(module => !idModuleDictionary.ContainsKey(module.idref)))
            {
                idModuleDictionary.Add(module.idref, module);
                parentSet.Add(module.parent);
            }

            foreach (var module in modules.Where(module => !parentSet.Contains(module.idref)).
                                           Where(module => module.@namespace.StartsWith("Urasandesu.NAnonym.DW")).
                                           Where(module => module.Relations.Any(relation => !parentSet.Contains(relation.to) &&
                                                                                            idModuleDictionary[relation.to].@namespace.StartsWith("Urasandesu.NAnonym.DW"))).

                                           Where(module => module.Relations.Any(relation => !parentSet.Contains(relation.to) &&
                                                                                            idModuleDictionary[relation.to].@namespace.StartsWith("Urasandesu.NAnonym.Cecil.DW"))))
            {
                Console.WriteLine(module.@namespace + "." + module.name);
            }
        }

        static List<Module> GetModules(string path)
        {
            var modules = new List<Module>();
            using (var reader = new XmlTextReader(path))
            {
                var document = new XPathDocument(reader);
                var navigator = document.CreateNavigator();
                var moduleNodes = navigator.Select("//Module");

                while (moduleNodes.MoveNext())
                {
                    var module = new Module();
                    module.idref = moduleNodes.Current.GetAttribute("idref", string.Empty);
                    module.name = moduleNodes.Current.GetAttribute("name", string.Empty);
                    module.parent = moduleNodes.Current.GetAttribute("parent", string.Empty);
                    module.@namespace = moduleNodes.Current.GetAttribute("namespace", string.Empty);

                    var relationNodes = moduleNodes.Current.Select("./Relation");
                    while (relationNodes.MoveNext())
                    {
                        var relation = new Relation();
                        relation.to = relationNodes.Current.GetAttribute("to", string.Empty);
                        relation.weight = relationNodes.Current.GetAttribute("weight", string.Empty);
                        module.Relations.Add(relation);
                    }
                    modules.Add(module);
                }
            }
            return modules;
        }
    }

    class Module
    {
        public Module()
        {
            Relations = new List<Relation>();
        }

        public string idref { get; set; }
        public string name { get; set; }
        public string parent { get; set; }
        public string @namespace { get; set; }
        public List<Relation> Relations { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var that = default(Module);
            if ((that = obj as Module) == null) return false;
            return idref.NullableEquals(that.idref) && name.NullableEquals(that.name);
        }

        public override int GetHashCode()
        {
            return idref.NullableGetHashCode() ^ name.NullableGetHashCode();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("idref = ");
            sb.Append(idref);
            sb.Append(", name = ");
            sb.Append(name);
            sb.Append(", parent = ");
            sb.Append(parent);
            sb.Append(", namespace = ");
            sb.Append(@namespace);
            sb.Append(", Relations = {");
            sb.Append(string.Join(", ", Relations.Select(relation => relation.ToString()).ToArray()));
            sb.Append("}");
            return sb.ToString();
        }
    }

    class Relation
    {
        public string to { get; set; }
        public string weight { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var that = default(Relation);
            if ((that = obj as Relation) == null) return false;
            return to.NullableEquals(that.to);
        }

        public override int GetHashCode()
        {
            return to.NullableGetHashCode();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("to = ");
            sb.Append(to);
            sb.Append(", weight = ");
            sb.Append(weight);
            return sb.ToString();
        }
    }
}
