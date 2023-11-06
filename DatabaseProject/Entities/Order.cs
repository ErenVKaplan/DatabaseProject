using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseProject.Entities
{
    public class Order
    {
        //public Order() 
        //{
        //    Orders= new HashSet<CartItem>(); //Her bir sipariş için bu sepetin içinde tutulacak bir Hashset oluşturacağız.Ürünler bunun içine eklenecek.
        //    HashSet ne öğrenmek istiyorsan:https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=net-7.0
        //}
        [Key]
        public int OrderId { get; set; }
        public int AdrresId { get; set; } //Adreslerle yapılacak bağlantılar için
        public int UserId { get; set; } //Kullanıcı üzerinden yapılacak işlemler için 
        public int OrderScore { get; set; } = 5; //Her siparişten 5 puan alması gerekiyor kullanıcının
        public string PaymentStatus { get; set; }  //Odeme tamamlandı ve sipariş iptal edildi 

        public double OrderFee { get; set; } //Ödeme ücreti

        public DateTime OrderDate { get; set; }


        [ForeignKey("AdrresId")] // AdrresId özelliği için yabancı anahtar ilişkisi tanımlanıyor
        public Adrress Adrress { get; set; }

        [ForeignKey("UserId")] // UserId özelliği için yabancı anahtar ilişkisi tanımlanıyor
        public User User { get; set; }


    }
}
