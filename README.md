# 🎯 ASP.NET Core MVC Projesi

Bu proje, **ASP.NET Core MVC** ve **Katmanlı Mimari** kullanılarak geliştirilmiş bir web uygulamasıdır. **Entity Framework Core**, **Identity** ve **Role-Based Authorization** ile güvenli kimlik doğrulama sağlanmaktadır. Kullanıcı dostu bir arayüz sunmak için **Responsive Design** ilkeleri uygulanmıştır.

---

## 🚀 Teknolojiler
- **ASP.NET Core MVC**
- **Katmanlı Mimari (Layered Architecture)**
- **Entity Framework Core**
- **Entity Framework Identity**
- **Authentication ve Role-Based Authorization**
- **Microsoft SQL Server**
- **Responsive Design**

---

## 📌 Proje Yapısı
Bu proje, **Katmanlı Mimari** prensiplerine uygun olarak geliştirilmiştir:

- **Presentation Layer (UI)** → Kullanıcı arayüzü ve MVC bileşenleri.
- **Business Layer** → İş mantığı ve servisler.
- **Data Access Layer** → Entity Framework Core kullanılarak veritabanı işlemleri.
- **Core Layer** → Temel modeller ve yardımcı bileşenler.

---

## 🔐 Kimlik Doğrulama
Projede **Entity Framework Identity** kullanılarak kullanıcı yönetimi sağlanmıştır. 
Kullanıcılar giriş yapabilir, kayıt olabilir ve rollerine göre yetkilendirme yapılabilir.

### 🛠 Yetkilendirme Özellikleri:
- Kullanıcı kayıt/giriş işlemleri
- Role-Based Authorization ile farklı yetkilendirme seviyeleri

---

## 📌 Kurulum ve Çalıştırma
1. **Projeyi Klonlayın:**
   ```sh
   git clone https://github.com/gurhanbatmaca/dotnet-core-mvc-project.git
   cd dotnet-core-mvc-project
   ```
2. **Bağımlılıkları Yükleyin:**
   ```sh
   dotnet restore
   ```
3. **Veritabanı Göçlerini Çalıştırın:**
   ```sh
   dotnet ef database update
   ```
4. **Uygulamayı Başlatın:**
   ```sh
   dotnet run
   ```

---

## 📌 Responsive Design
Uygulama, masaüstü ve mobil cihazlarla uyumlu olacak şekilde **Responsive Design** prensipleri kullanılarak geliştirilmiştir. **Bootstrap** ve **CSS media queries** ile kullanıcı dostu bir deneyim sunulmaktadır.

---

## 🛠 Katkıda Bulunma
Her türlü katkıya açığız! Fork alıp geliştirme yapabilir ve pull request gönderebilirsiniz. Sorularınız için [Gürhan Batmaca](https://github.com/gurhanbatmaca) ile iletişime geçebilirsiniz.

🚀 **İyi kodlamalar!**

