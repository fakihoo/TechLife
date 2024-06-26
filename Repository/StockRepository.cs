using TechLife.Data;
using TechLife.Models.DTOs;
using TechLife.Models;
using Microsoft.EntityFrameworkCore;

namespace TechLife.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByBookId(int shopeStoreId) => await _context.Stocks.FirstOrDefaultAsync(s => s.ShopStoreId == shopeStoreId);

        public async Task ManageStock(StockDTO stockToManage)
        {
            // if there is no stock for given book id, then add new record
            // if there is already stock for given book id, update stock's quantity
            var existingStock = await GetStockByBookId(stockToManage.ShopStoreId);
            if (existingStock is null)
            {
                var stock = new Stock { ShopStoreId = stockToManage.ShopStoreId, Quantity = stockToManage.Quantity };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from ShopStore in _context.ShopStores
                                join stock in _context.Stocks
                                on ShopStore.Id equals stock.ShopStoreId
                                into ShopStore_stock
                                from ShopStoreStock in ShopStore_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) || ShopStore.Name.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    ShopStoreId = ShopStore.Id,
                                    ShopeStoreName = ShopStore.Name,
                                    Quantity = ShopStoreStock == null ? 0 : ShopStoreStock.Quantity
                                }
                                ).ToListAsync();
            return stocks;
        }
        public async Task UpdateStockFromSupplierItem(SupplierItem supplierItem)
        {
            var existingStock = await GetStockByBookId((int)supplierItem.ShopStoreId);
            if (existingStock is null)
            {
                var stock = new Stock
                {
                    ShopStoreId = (int)supplierItem.ShopStoreId,
                    Quantity = supplierItem.Quantity,
                    SupplierItemId = supplierItem.SupplierItemId
                };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity += supplierItem.Quantity;
                existingStock.SupplierItemId = supplierItem.SupplierItemId;
            }
            await _context.SaveChangesAsync();
        }

    }

    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task<Stock?> GetStockByBookId(int shopStoreId);
        Task ManageStock(StockDTO stockToManage);
        Task UpdateStockFromSupplierItem(SupplierItem supplierItem);
    }
}

