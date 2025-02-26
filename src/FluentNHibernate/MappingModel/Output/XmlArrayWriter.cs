using System.Xml;
using FluentNHibernate.MappingModel.Collections;
using FluentNHibernate.Utils;

namespace FluentNHibernate.MappingModel.Output;

public class XmlArrayWriter : BaseXmlCollectionWriter, IXmlWriter<CollectionMapping>
{
    readonly IXmlWriterServiceLocator serviceLocator;

    public XmlArrayWriter(IXmlWriterServiceLocator serviceLocator)
        : base(serviceLocator)
    {
        this.serviceLocator = serviceLocator;
    }

    public XmlDocument Write(CollectionMapping mappingModel)
    {
        document = null;
        mappingModel.AcceptVisitor(this);
        return document;
    }

    public override void ProcessCollection(CollectionMapping mapping)
    {
        document = new XmlDocument();

        var element = document.AddElement("array");

        WriteBaseCollectionAttributes(element, mapping);
    }

    public override void Visit(IIndexMapping indexMapping)
    {
        var writer = serviceLocator.GetWriter<IIndexMapping>();
        var xml = writer.Write(indexMapping);

        document.ImportAndAppendChild(xml);
    }
}
