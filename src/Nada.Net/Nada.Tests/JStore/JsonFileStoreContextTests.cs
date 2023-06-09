﻿using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Nada.Core.JStore;
using NUnit.Framework;

namespace Nada.Core.Tests.JStore;

[TestFixture]
internal class JsonFileStoreContextTests
{
    [ExcludeFromCodeCoverage]
    internal class MockFileStore : IFileStore
    {
        public string Read(string path)
        {
            if (path == "Person.json")
                return PersonJson;

            throw new FileNotFoundException($"Cannot find file {path}", path);
        }

        public void Save(string path, string source)
        {
            if (path == "Person.json")
                PersonJsonSaved = source;
            else
                throw new FileNotFoundException($"Cannot find file {path}", path);
        }

        #region JsonExamples

        private const string PersonJson =
            @"[{""Id"":""b10affbe-54b6-4968-8d0f-dc049b68bf3f"",""Name"":""Bruce Banner""},{""Id"":""e2278fe6-390b-4633-8018-cf09a2b56e66"",""Name"":""Toni Stark""}]";

        internal string PersonJsonSaved = string.Empty;

        #endregion
    }

    private class NotExisting
    {
    }

    private class Person
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    [Test]
    public void Read_From_Filesystem()
    {
        var person1 = new Person { Id = Guid.Parse("b10affbe-54b6-4968-8d0f-dc049b68bf3f"), Name = "Bruce Banner" };
        var person2 = new Person { Id = Guid.Parse("e2278fe6-390b-4633-8018-cf09a2b56e66"), Name = "Toni Stark" };
        var expected = new[] { person1, person2 };

        var fileReader = new MockFileStore();
        var sut = new JsonFileStoreContext(fileReader);
        var actual = sut.Get<Person>();

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Write_To_Filesystem()
    {
        var expected =
            @"[{""Id"":""aaaaffbe-54b6-4968-8d0f-dc049b68bf3f"",""Name"":""Bruce""},{""Id"":""aaaa8fe6-390b-4633-8018-cf09a2b56e66"",""Name"":""Toni""}]";
        var person1 = new Person { Id = Guid.Parse("aaaaffbe-54b6-4968-8d0f-dc049b68bf3f"), Name = "Bruce" };
        var person2 = new Person { Id = Guid.Parse("aaaa8fe6-390b-4633-8018-cf09a2b56e66"), Name = "Toni" };
        var people = new[] { person1, person2 };

        var fileReader = new MockFileStore();
        var sut = new JsonFileStoreContext(fileReader);
        sut.Save(people);

        fileReader.PersonJsonSaved.Should().Be(expected);
    }

    [Test]
    public void NotReThrow_FileStore_Exception()
    {
        var fileReader = new MockFileStore();
        var sut = new JsonFileStoreContext(fileReader);
        var a = () => { _ = sut.Get<NotExisting>(); };
        a.Should().Throw<FileNotFoundException>().WithMessage("Cannot find file NotExisting.json");
    }
}