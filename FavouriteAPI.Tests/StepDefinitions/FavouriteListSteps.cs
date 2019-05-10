using TechTalk.SpecFlow;
using System.Collections.Generic;
using FluentAssertions;
using System;
using System.Linq;

namespace MyFavouriteAPI.Tests.StepDefinitions
{
    [Binding]
    public class FavouriteListSteps
    {
        private readonly IFavouriteService _favouriteService;
        private int _favouriteListId;
        private readonly int _userId;

        public FavouriteListSteps()
        {
            _favouriteService = new FavouriteService();
            _userId = 1;
        }

        [When(@"I create a new favourite list")]
        public void WhenICreateANewFavouriteList()
        {
            _favouriteListId = _favouriteService.Create(_userId);
        }

        [Then(@"the favourite list should be empty")]
        public void ThenTheFavouriteListShouldBeEmpty()
        {
            FavouriteList favouriteList = _favouriteService.GetFavouriteList(_userId, _favouriteListId);

            favouriteList.Should().NotBeNull();
            favouriteList.FavouriteListId.Should().Be(_favouriteListId);
            favouriteList.ProductIds.Should().BeNull();

        }
    }

    public class FavouriteList
    {
        public int FavouriteListId { get; set; }
        public List<int> ProductIds { get; set; }
    }

    public interface IFavouriteService
    {
        int Create(int userId);
        FavouriteList GetFavouriteList(int userId, int favouriteListId);
    }

    public class FavouriteService : IFavouriteService
    {
        private readonly Dictionary<int, List<FavouriteList>> favouriteListStore = new Dictionary<int, List<FavouriteList>>();

        public int Create(int userId)
        {
            int favouriteListId = new Random().Next(10);

            var newFavouriteList = new List<FavouriteList>
            {
                new FavouriteList { FavouriteListId = favouriteListId }
            };

            favouriteListStore.Add(userId, newFavouriteList);

            return favouriteListId;
        }

        public FavouriteList GetFavouriteList(int userId, int favouriteListId)
        {
            if (favouriteListStore.TryGetValue(userId, out List<FavouriteList> userFavouriteList))
            {
                var favouriteList = userFavouriteList.FirstOrDefault(_ => _.FavouriteListId == favouriteListId);

                return favouriteList;
            }

            return null;
        }
    }
}