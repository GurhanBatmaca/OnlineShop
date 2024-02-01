using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Extentions
{
    public static class ModelBuilderExtention
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product {
                    Id = 1,
                    Name = "Eski Kars Kaşarı",
                    Price = 300,
                    Description = "Eski Kars Kaşarı Açıklaması",
                    Url = "eksi-kars-kasari",
                    ImageUrl = "1.jpg",
                    IsApproved = true,
                    IsHome = true,
                    StockQuantity = 100,
                    Weight = 1000
                },
                new Product {
                    Id = 2,
                    Name = "Yeni Kars Kaşarı",
                    Price = 280,
                    Description = "Yeni Kars Kaşarı Açıklaması",
                    Url = "yeni-kars-kasari",
                    ImageUrl = "2.jpg",
                    IsApproved = true,
                    IsHome = true,
                    StockQuantity = 100,
                    Weight = 1000
                },
                new Product {
                    Id = 3,
                    Name = "Natural Beyaz Peynir",
                    Price = 250,
                    Description = "Natural Beyaz Peynir Açıklaması",
                    Url = "natural-beyaz-peynir",
                    ImageUrl = "3.jpg",
                    IsApproved = true,
                    IsHome = true,
                    StockQuantity = 100,
                    Weight = 1000
                },
                new Product {
                    Id = 4,
                    Name = "Kara Kovan Petek Bal",
                    Price = 550,
                    Description = "Kara Kovan Petek Bal Açıklaması",
                    Url = "kara-kovan-petek-bal",
                    ImageUrl = "4.jpg",
                    IsApproved = true,
                    IsHome = true,
                    StockQuantity = 100,
                    Weight = 1200
                },
                new Product {
                    Id = 5,
                    Name = "Süzme Bal",
                    Price = 500,
                    Description = "Süzme Bal Açıklaması",
                    Url = "suzme-bal",
                    ImageUrl = "5.jpg",
                    IsApproved = true,
                    IsHome = true,
                    StockQuantity = 100,
                    Weight = 1000
                },
                new Product {
                    Id = 6,
                    Name = "Çeçil Peyniri",
                    Price = 260,
                    Description = "Çeçil Peyniri Açıklaması",
                    Url = "cecil-peyniri",
                    ImageUrl = "6.jpg",
                    IsApproved = true,
                    IsHome = true,
                    StockQuantity = 100,
                    Weight = 1000
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category {Id = 1,Name="Kaşar",Url="kasar"},
                new Category {Id = 2,Name="Bal",Url="bal"},
                new Category {Id = 3,Name="Peynir",Url="peynir"},
                new Category {Id = 4,Name="Eski Kaşar",Url="eski-kasar"},
                new Category {Id = 5,Name="Yeni Kaşar",Url="yeni-kasar"},
                new Category {Id = 6,Name="Kara Kovan Bal",Url="kara-kovan-bal"},
                new Category {Id = 7,Name="Petek Bal",Url="petek-bal"},
                new Category {Id = 8,Name="Süzme Bal",Url="suzme-bal"}
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory {ProductId = 1,CategoryId = 1},
                new ProductCategory {ProductId = 1,CategoryId = 3},
                new ProductCategory {ProductId = 1,CategoryId = 4},
                new ProductCategory {ProductId = 2,CategoryId = 1},
                new ProductCategory {ProductId = 2,CategoryId = 3},
                new ProductCategory {ProductId = 2,CategoryId = 5},
                new ProductCategory {ProductId = 3,CategoryId = 3},
                new ProductCategory {ProductId = 4,CategoryId = 2},
                new ProductCategory {ProductId = 4,CategoryId = 6},
                new ProductCategory {ProductId = 4,CategoryId = 7},
                new ProductCategory {ProductId = 5,CategoryId = 2},
                new ProductCategory {ProductId = 5,CategoryId = 8},
                new ProductCategory {ProductId = 6,CategoryId = 3}
            );

            modelBuilder.Entity<OrderState>().HasData(
                new OrderState {Id=1,Name="Beklemede",Url="beklemede"},
                new OrderState {Id=2,Name="Hazırlanıyor",Url="hazirlaniyor"},
                new OrderState {Id=3,Name="Kargoda",Url="kargoda"},
                new OrderState {Id=4,Name="Teslim Edildi",Url="teslim-edildi"},
                new OrderState {Id=5,Name="İptal Edildi",Url="iptal-edildi"}
            );
        }
    }
}