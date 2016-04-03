using System.Runtime.Serialization;
using System.IO;
using System;
using System.Reflection;
using System.Collections;

namespace hunger_games_simulator
{

    public class IniFormatter : IFormatter
    {
        #region Properties
        public ISurrogateSelector SurrogateSelector { get; set; }

        public SerializationBinder Binder { get; set; }

        public StreamingContext Context { get; set; }
        #endregion


        #region Constructors
        public IniFormatter()
        {
            Context = new StreamingContext(StreamingContextStates.All);
        }
        #endregion


        #region IFormatter Members
        public object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Stream serializationStream, object graph)
        {
            var propertyInfos = graph.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            StreamWriter sw = new StreamWriter(serializationStream);

            foreach (var propertyInfo in propertyInfos)
            {
                if (!propertyInfo.CanRead)
                {
                    continue;
                }

                if (Attribute.IsDefined(propertyInfo, typeof(NonSerializedAttribute)))
                {
                    continue;
                }

                if (propertyInfo.PropertyType.IsPrimitive)
                {
                    sw.WriteLine("{0}={1}", propertyInfo.Name, propertyInfo.GetValue(graph, null));
                }

                //object/complex types need to recursively call this method until the end of the tree is reached
                else
                {
                    var complexType = GetPropertyValue(graph, propertyInfo.Name);
                    Serialize(serializationStream, complexType);
                }
            }

            sw.Close();
        }
        #endregion


        public static object GetPropertyValue(object sourceObject, string propertyName)
        {
            if (sourceObject == null)
            {
                return null;
            }

            object obj = sourceObject;

            // Split property name to parts (propertyName could be hierarchical, like obj.subobj.subobj.property
            string[] propertyNameParts = propertyName.Split('.');

            foreach (string propertyNamePart in propertyNameParts)
            {
                if (obj == null)
                {
                    return null;
                }

                // propertyNamePart could contain reference to specific 
                // element (by index) inside a collection
                if (!propertyNamePart.Contains("["))
                {
                    PropertyInfo pi = obj.GetType().GetProperty(propertyNamePart, BindingFlags.Public | BindingFlags.Instance);
                    if (pi == null)
                    {
                        return null;
                    }

                    obj = pi.GetValue(obj, null);
                }
                else
                {
                    // propertyNamePart is a reference to specific element 
                    // (by index) inside a collection
                    // like AggregatedCollection[123]
                    // get collection name and element index
                    int indexStart = propertyNamePart.IndexOf("[") + 1;
                    string collectionPropertyName = propertyNamePart.Substring(0, indexStart - 1);
                    int collectionElementIndex = Int32.Parse(propertyNamePart.Substring(indexStart, propertyNamePart.Length - indexStart - 1));
                    //   get collection object
                    PropertyInfo pi = obj.GetType().GetProperty(collectionPropertyName, BindingFlags.Public | BindingFlags.Instance);
                    if (pi == null)
                    {
                        return null;
                    }

                    object unknownCollection = pi.GetValue(obj, null);
                    //   try to process the collection as array
                    if (unknownCollection.GetType().IsArray)
                    {
                        object[] collectionAsArray = unknownCollection as Array[];
                        obj = collectionAsArray[collectionElementIndex];
                    }
                    else
                    {
                        //   try to process the collection as IList
                        IList collectionAsList = unknownCollection as IList;
                        if (collectionAsList != null)
                        {
                            obj = collectionAsList[collectionElementIndex];
                        }
                        else
                        {
                            // ??? Unsupported collection type
                        }
                    }
                }
            }

            return obj;
        }
    }
}