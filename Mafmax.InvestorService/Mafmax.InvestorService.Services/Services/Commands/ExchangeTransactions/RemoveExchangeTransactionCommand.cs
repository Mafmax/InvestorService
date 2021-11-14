﻿using MediatR;

namespace Mafmax.InvestorService.Services.Services.Commands.ExchangeTransactions;

/// <summary>
/// Command to delete transaction from portfolio
/// </summary>
public record RemoveExchangeTransactionCommand(int InvestorId, int PortfolioId, int TransactionId) 
    : IRequest;