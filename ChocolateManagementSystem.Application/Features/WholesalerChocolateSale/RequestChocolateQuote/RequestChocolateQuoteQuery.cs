using AutoMapper;
using ChocolateManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.RequestChocolateQuote;

public class OrderItem
{
    public int ChocolateBarId { get; set; }
    public int Quantity { get; set; }
}

public class RequestChocolateQuoteQuery: IRequest<RequestChocolateQuoteResponse>
{
    public int WholesalerId { get; set; }
    public IEnumerable<OrderItem> OrderedItems { get; set; }
}

public class RequestChocolateQuoteQueryHandler : IRequestHandler<RequestChocolateQuoteQuery, RequestChocolateQuoteResponse>
{
    private readonly IChocolateBarsRepository _chocolateBarsRepository;
    private readonly IMapper _mapper;

    public RequestChocolateQuoteQueryHandler(IChocolateBarsRepository chocolateBarsRepository, IMapper mapper)
    {
        _chocolateBarsRepository = chocolateBarsRepository;
        _mapper = mapper;
    }

    public async Task<RequestChocolateQuoteResponse> Handle(RequestChocolateQuoteQuery request, CancellationToken cancellationToken)
    {
        var chocolateBarsIds = request.OrderedItems.Select(x => x.ChocolateBarId).ToHashSet();
        var chocolateQuantityDic = new Dictionary<int, int>();
        var totalQuantity = 0;
        foreach (var item in request.OrderedItems)
        {
            chocolateQuantityDic[item.ChocolateBarId] = item.Quantity;
            totalQuantity += item.Quantity;
        }

        // discount 
        var discount = FetchDiscount(totalQuantity);

        // fetch chocolate prices 
        var dbChocolateBars = await _chocolateBarsRepository.FetchByIds(chocolateBarsIds, cancellationToken);
        var chocolatePriceDic = new Dictionary<int, decimal>();
        foreach (var c in dbChocolateBars)
        {
            chocolatePriceDic[c.Id] = c.Price;
        }

        // calculate price 
        decimal price = 0;
        foreach (var chocolateId in chocolateBarsIds)
        {
            var quantity = chocolateQuantityDic[chocolateId];
            var itemPrice = chocolatePriceDic[chocolateId];
            price += quantity * itemPrice;
        }

        if(discount > 1)
        {
            var discountAmount = (price * discount) / 100;
            price -= discountAmount;
        }

        return new RequestChocolateQuoteResponse
        {
            Price = price,
            QuoteSummary = FetchQuoteSummary(discount)
        };
        
    }

    private int FetchDiscount(int totalQuantity)
    {
        if(totalQuantity > 10 && totalQuantity < 20) {
            return 10;
        } 
        else if (totalQuantity > 20)
        {
            return 20;
        }
        return 1;
    }

    private string FetchQuoteSummary(int discount)
    {
       if(discount > 1)
       {
            return $"{discount}% discount was applied,because you did buy more than {discount} chocolates.";
       }
       else
       {
            return "no discount was applied,because you did buy less than 10 chocolates.";
       }
    }
}
