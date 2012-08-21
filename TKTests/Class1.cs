//using System;
//using System.Data;
//using ACRL.EFData;
//using ACRL.EFData;
//using NUnit.Framework;
//using TKCommon.Serialization;
//using TKMVC.Controllers;
//using TKModel;

//namespace TKTests
//{

//    [TestFixture]
//    public class CommonTests
//    {



//        [Test]
//        public void TestUndo()
//        {
//            var dbf = new DatabaseFactory();
//            var repo = new Repository<SavedUndoCommand>(dbf);


//            var r = new Resource {Id = Guid.NewGuid(), Name = "Test Resource"};

//            var c = new UpdateResourceCommand {Resource = r};

//            var ser = SerializeCommand(c);
//            var suc = new SavedUndoCommand
//                          {
//                              Id = Guid.NewGuid(), 
//                              CreatedDate = DateTime.Now, 
//                              SerializedCommand = ser
//                          };

//            repo.Add(suc);
//            repo.Save();

//        }



//        [Test]
//        public void TestRet()
//        {
//            var dbf = new DatabaseFactory();
//            var repo = new Repository<SavedUndoCommand>(dbf);


//            var c = repo.Get(x => x.Id == new Guid("A4AF87F3-A79B-4879-8CD6-BC1A67F5CB5D"));

//            var u = new UpdateResourceCommand();
//            var typeRegistry = new XmlStringSerializableTypeRegistry();
//            typeRegistry.RegisterType(u.GetType());

//            var de = new XmlStringDeserializer(typeRegistry);

//            var d = de.Deserialize(c.SerializedCommand) as UpdateResourceCommand;

//            Assert.AreEqual("Test Resource", d.Resource.Name);
            
//        }

//        string SerializeCommand(ICommand undoCommand)
//        {
//            var ser = new XmlStringSerializer();
//            return ser.Serialize(undoCommand);
//        }


//        [Test]
//        public void Test()
//        {
//            var dbf = new DatabaseFactory();
//            var repo = new Repository<Resource>(dbf);

//            var resource = repo.GetById(new Guid("9145E4F0-4D9C-4C29-5501-00E3BB78AB5C"));
//            Assert.AreEqual(resource.Name, "ufbvrfsh.txt");

//            var notLoaded = repo.DbContext.Entry(resource).Collection<Category>(x => x.Categories).IsLoaded;
//            Assert.IsFalse(notLoaded);
//            repo.DbContext.Entry(resource).Collection<Category>(x => x.Categories).Load();
//            notLoaded = repo.DbContext.Entry(resource).Collection<Category>(x => x.Categories).IsLoaded;
//            Assert.IsTrue(notLoaded);

//            //  var rToRemove = resource.Categories.Single(x => x.Id == new Guid("8413D734-24E4-53AA-38FF-7BA6B8CF19BA"));
//            //  resource.Categories.Remove(rToRemove);

//            Assert.AreEqual(0, resource.Categories.Count);
//            var newCat = new Category { Id = new Guid("8413D734-24E4-53AA-38FF-7BA6B8CF19BA") };

//            resource.Categories.Add(newCat);
//            repo.DbContext.Entry(newCat).State = EntityState.Unchanged;//unattached object

//            //Assert.AreEqual("Seafood", newCat.Name);


//            repo.Update(resource);
//            repo.Save();
//        }

//    }
//}
