using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assessment.Model.Domain;
using Assessment.Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Assessment.Domain;
using Assessment.DataLayer;

namespace Assessment.Tests
{
    [TestClass]
    public class SortingTests
    {
        //The first should show the frequency of the first and last names ordered by frequency descending and 
        //then alphabetically ascending.
        [TestMethod]
        public void TestSortByNames()
        {
            var domain = new ContactDomain();
            var context = new Mock<AppDataContext>();
            context.SetupGet(x => x.Contacts)
                .Returns(() =>
                {
                    return new Repository<Contact> ( new List<Contact>()
                    {
                        new Contact { FirstName = "John", LastName = "Doe"  },
                        new Contact { FirstName = "Peter", LastName = "Botha"  },
                        new Contact { FirstName = "Sarie", LastName = "Coetzee"  },
                        new Contact { FirstName = "Gerrit", LastName = "de Bruin"  },
                        new Contact { FirstName = "Jan", LastName = "vd Merwe"  },
                        new Contact { FirstName = "Sarel", LastName = "Doe"  },
                        new Contact { FirstName = "Koos", LastName = "Smith"  },
                        new Contact { FirstName = "Sannie", LastName = "Smith"  }
                    });
                });
            IEnumerable<KeyValuePair<int,Contact>> result = domain.SortByNamesAndFrequency(context.Object);

            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreUnique(result.ToList());
            Assert.IsTrue(result.Any(), "Returned No Resultes");
            Assert.IsTrue(result.Count() == 6, "Result count mismatch");
            Assert.IsTrue(result.First().Key == 2, "First element count mismatch") ;
            Assert.IsTrue(result.First().Value.LastName == "Doe", "First element last name mismatch");
            Assert.IsTrue(result.Last().Key == 1, "Last element count mismatch");
            Assert.IsTrue(result.Last().Value.LastName == "vd Merwe", "Last element last name mismatch");

        }


        [TestMethod]
        public void TestGetSortedListByNamesAndFrequency()
        {
            var domain = new ContactDomain();
            var context = new Mock<AppDataContext>();
            context.SetupGet(x => x.Contacts)
                .Returns(() =>
                {
                    return new Repository<Contact>(new List<Contact>()
                    {
                        new Contact { FirstName = "John", LastName = "Doe"  },
                        new Contact { FirstName = "Peter", LastName = "Botha"  },
                        new Contact { FirstName = "Sarie", LastName = "Coetzee"  },
                        new Contact { FirstName = "Gerrit", LastName = "de Bruin"  },
                        new Contact { FirstName = "Jan", LastName = "vd Merwe"  },
                        new Contact { FirstName = "Sarel", LastName = "Doe"  },
                        new Contact { FirstName = "Koos", LastName = "Smith"  },
                        new Contact { FirstName = "Sannie", LastName = "Smith"  }
                    });
                });
            IEnumerable<KeyValuePair<int, string>> result = domain.GetSortedListByNamesAndFrequency(context.Object);

            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreUnique(result.ToList());
            Assert.IsTrue(result.Any(), "Returned No Resultes");
            Assert.IsTrue(result.Count() == 14, "Result count mismatch");
            Assert.IsTrue(result.First().Key == 2, "First element count mismatch");
            Assert.IsTrue(result.First().Value == "Doe", "First element last name mismatch");
            Assert.IsTrue(result.Last().Key == 1, "Last element count mismatch");
            Assert.IsTrue(result.Last().Value== "vd Merwe", "Last element last name mismatch");

        }

        //The second should show the addresses sorted alphabetically by street name.
        //So ordered by frequency first (descending) and then alphabetically (ascending).
        [TestMethod]
        public void TestSortByAddresses()
        {
            var domain = new AddressDomain();
            var context = new Mock<IAppDataContext>();
            context.SetupGet(x => x.Addresses)
                .Returns(() =>
                {
                    return new Repository<Address>(new List<Address>()
                    {
                        new Address() { Number = 102,  Street = "Long Lane" },
                        new Address() { Number = 65, Street = "Ambling Way" },
                        new Address() { Number = 82, Street = "Stewart St" },
                        new Address() { Number = 12, Street = "Howard St" },
                        new Address() { Number = 78, Street = "Short Lane" },
                        new Address() { Number = 49, Street = "Sutherland St" },
                    });
                });
            IEnumerable<string> result = domain.SortByStreetAndFrequency(context.Object);

            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreUnique(result.ToList());
            Assert.IsTrue(result.Any(), "Returned No Resultes");
            Assert.IsTrue(result.Count() == 6, "Result count mismatch");

            Assert.IsTrue(result.First() == "65 Ambling Way", "First element mismatch");
            Assert.IsTrue(result.Last() == "49 Sutherland St", "Last element mismatch");
        }
    }
}
