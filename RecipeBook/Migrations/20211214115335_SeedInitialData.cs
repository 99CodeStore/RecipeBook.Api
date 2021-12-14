using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBook.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[] { 1L, "जलेबी उत्तर भारत, पाकिस्तान व मध्यपूर्व का एक लोकप्रिय व्यंजन है। इसका आकार पेंचदार होता है और स्वाद करारा मीठा। इस मिठाई की धूम भारतीय उपमहाद्वीप से शुरू होकर पश्चिमी देश स्पेन तक जाती है। इस बीच भारत, बांग्लादेश, पाकिस्तान, ईरान के साथ तमाम अरब मुल्कों में भी यह खूब जानी-पहचानी है।", "https://punampaul.com/wp-content/uploads/2019/10/Jalebi-Recipe.jpg", "जलेबी" });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[] { 2L, "समोसा एक तला हुआ या बेक किया हुआ भरवां अल्पाहार व्यंजन है। इसमें प्रायः मसालेदार भुने या पके हुए सूखे आलू, या इसके अलावा मटर, प्याज, दाल, कहीम कहीं मांसा भी भरा हो सकता है। इसका आकार प्रायः तिकोना होता है किन्तु आकार और नाप भिन्न-भिन्न स्थानों पर बदल सकता है। अधिकतर ये चटनी के संग परोसे जाते हैं।", "https://static.langimg.com/thumb/msid-76956747,width-680,resizemode-3/navbharat-times.jpg", "समोसा" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
