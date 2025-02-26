using System;
using System.Linq;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.ClassBased;
using NUnit.Framework;

namespace FluentNHibernate.Testing.FluentInterfaceTests;

[TestFixture]
public class SubclassMapForJoinedSubclassConventionTests
{
    [Test]
    public void ShouldAllowSettingOfKeyInConvention()
    {
        var model = new PersistenceModel();

        var parent = new ClassMap<Parent>();
        parent.Id(x => x.Id);
        var child = new SubclassMap<Child>();

        model.Add(parent);
        model.Add(child);
        model.Conventions.Add<SCKeyConvention>();

        var subclass = model.BuildMappings()
            .SelectMany(x => x.Classes)
            .First()
            .Subclasses.First();

        subclass.Key.Columns.First().Name.ShouldEqual("xxx");
        subclass.Key.Columns.Count().ShouldEqual(1);
    }

    private class SCKeyConvention : IJoinedSubclassConvention
    {
        public void Apply(IJoinedSubclassInstance instance)
        {
            instance.Key.Column("xxx");
        }
    }

    private class Parent 
    {
        public int Id { get; set; }
    }

    private class Child : Parent
    {}
}
