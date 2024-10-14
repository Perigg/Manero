
![Alt text](https://github.com/Perigg/Manero/blob/master/order.png) ![Alt text](https://github.com/Perigg/Manero/blob/master/cart.png)![Alt text](https://github.com/Perigg/Manero/blob/master/checkout.png)


# Manero

Detta projekt är ett grupparbete där målet är att bygga en webbutik med användning av Blazor och deployment i Azure. Projektet använder microservices-arkitektur för att hantera olika aspekter av webbutiken, mina microservices består av kundvagn, orderhantering och checkout-processen. Vi han inte knyta ihop projektet helt då medlemmar inte blev färdiga i tid eller hoppade av.

## Microservices

Varje service hanterar olika aspekter av affärslogiken och de är byggda för att fungera tillsammans för att hantera användarens köpprocess från start till slut. Alla microservices har deployats i Azure, vilket möjliggör skalbarhet och flexibilitet i hanteringen av webbutikens olika funktioner.

### Cart Service

Cart Service hanterar användarens varukorg och interaktioner relaterade till att lägga till och ta bort produkter från varukorgen.

**Teknologi och ramverk:**
- Azure Functions
- .NET 8
- Azure SQL Database

**Funktioner:**
- Lägg till produkt i varukorg
- Ta bort produkt från varukorg
- Uppdatera antal av en produkt
- Hämta alla produkter i varukorgen

### Order Service

Order Service skapar och hanterar beställningar baserade på innehållet i användarens varukorg.

**Teknologi och ramverk:**
- ASP.NET Core Web API
- Entity Framework Core
- Azure Service Bus för att kommunicera med andra tjänster

**Funktioner:**
- Skapa en ny order
- Hämta detaljer för specifika ordrar
- Uppdatera orderstatus

### Checkout Service

Checkout Service hanterar slutpunkten för köpprocessen, inklusive betalning och orderbekräftelse.
