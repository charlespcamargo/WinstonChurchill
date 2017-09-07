
	SELECT leilao.ID											AS ID, 
		   leilao.Nome											AS NomeDoLeilao, 
           produto.Nome 										AS Produto, 
           leilao.DataFinalFormacao								AS DataFormacao,
           leilao.DataAbertura									AS DataAbertura,
           leilao.QtdDesejada									AS QtdDesejada,
           leilao.RodadasLeilao									AS TotalRodadas,
           /*criador.Nome										AS Criador,*/
           representante.Nome									AS Representante,          
           rodada.Numero										AS RodadaNumero,
           rodada.DataEncerramento								AS RodadaEncerramento,
           
           /*comprador.NomeFantasia								AS Comprador,*/
           fornecedor.NomeFantasia								AS Fornecedor,
           lances.ValorPrimeiraMargem							AS ValorPrimeiraMargem,
           lances.ValorSegundaMargem							AS ValorSegundaMargem
    
      FROM leilao 									AS leilao
      
      /* LEILAO  */
      JOIN produto									AS produto
        ON leilao.produtoID = produto.ID
	  JOIN usuario									AS criador
        ON leilao.CriadorID = criador.ID
	  JOIN usuario									AS representante
        ON leilao.RepresentanteID = representante.ID    
	  JOIN leilaorodada								AS rodada
        ON leilao.ID = rodada.leilaoID
      /* LEILAO  */
      
      /*      
      LEFT JOIN leilaocomprador		AS lc
        ON leilao.ID = lc.leilaoID
	  LEFT JOIN parceironegocio		AS comprador
        ON lc.parceironegocioID = comprador.ID  
	  */    
	  
      LEFT JOIN leilaofornecedor	AS leilao_fornecedor
        ON leilao.ID = leilao_fornecedor.leilaoID
	  LEFT JOIN parceironegocio		AS fornecedor
        ON leilao_fornecedor.parceironegocioID = fornecedor.ID
	  LEFT JOIN leilaofornecedorrodada				AS lances
        ON leilao.ID = lances.leilaofornecedorID
      
      
	 WHERE leilao.ID = 3