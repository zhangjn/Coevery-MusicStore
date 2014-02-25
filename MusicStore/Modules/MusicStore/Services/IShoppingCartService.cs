using System.Collections.Generic;
using Coevery;
using MusicStore.Models;

namespace MusicStore.Services {
    public interface IShoppingCartService : IDependency {
        void AddToCart(Album album);
        int RemoveFromCart(int id);
        void EmptyCart();
        List<Cart> GetCartItems();
        int GetCount();
        decimal GetTotal();
        int CreateOrder(Order order);
        void MigrateCart(string userName);
    }
}