# Bitirme Projesi: Dev Architecture (Backend)
Bu proje, Dev Architecture çatısını kullanarak geliştirilmiş bir backend uygulamasıdır. Proje, sipariş yönetim sistemi ve hata yönetim sistemi olmak üzere iki ana modülden oluşmaktadır. Her iki modül de .NET tabanlı olarak geliştirilmiştir ve Elasticsearch ve Kibana entegrasyonu ile log analizi ve görselleştirme sağlanmaktadır.

## Modüller
### 1. Sipariş Yönetim Sistemi
Bu modül, kullanıcılara sipariş verme, sipariş takibi yapma ve sipariş geçmişini görüntüleme gibi işlemleri gerçekleştirmelerini sağlar. Siparişlerin detayları ve durumları, veritabanında saklanır ve kullanıcı etkileşimleri analiz edilir.

### 2. Hata Yönetim Sistemi
Bu modül, kullanıcıların sistemde meydana gelen hataları bir form aracılığı ile gönderir.Yönetici olan kişiler hatayı anladıktan sonra açık/çözüldü/kapalı şeklinde işaretler.

## Kullanılan Araçlar ve Paketler
### 1. DevArchitecture
DevArchitecture Backend Template Pack'i, DevArchitecture çatısı altında backend geliştirme için kullanılan temel şablonları içerir. Bu şablonlar, projelerin hızlıca yapılandırılmasını sağlar ve geliştiricilerin backend süreçlerini düzenlemelerine yardımcı olur. Visual Studio ile kolayca entegre edilerek sağlam bir temel sunar.

### 2. DevArchitectureGen
DevArchitectureGen bir Kod Üreteci aracıdır. Backend geliştirme sürecinde model tabanlı yaklaşım kullanarak hızlıca kod üretmek için kullanılır. Bu araç, sürekli geliştirme yapan projelerde zaman kazanılmasına yardımcı olur ve yazılım geliştirme sürecini daha verimli hale getirir.

## Kurulum
### Prerequisites (Ön Koşullar)
Proje çalıştırılmadan önce, aşağıdaki yazılımların sisteminizde yüklü olduğundan emin olun:

.NET 5.0 veya daha yeni bir sürüm İndir

Adım 1: Projeyi Visual Studio'da Açın
Visual Studio'yu açın ve klonladığınız projeyi File > Open > Project/Solution seçeneği ile açın.
Proje açıldığında, gerekli NuGet paketlerini yüklemek için Tools > NuGet Package Manager > Restore Packages seçeneğini tıklayın.
Adım 2: Backend Kurulumu
Backend tarafını çalıştırmak için appsettings.json dosyasındaki ayarları kontrol edin. Örneğin, veritabanı bağlantı ayarlarını doğru bir şekilde yapılandırmalısınız.

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=mydb;Username=myuser;Password=mypassword"
  }
}

Ardından, projeyi Visual Studio üzerinde başlatabilirsiniz. F5 tuşuna basarak API'nizi https://localhost:5001 adresinde çalıştırabilirsiniz.

