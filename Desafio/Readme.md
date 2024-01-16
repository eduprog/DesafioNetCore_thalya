
 - [x] Compilando
 - [x] Projeto organizado
 - [x] F�cil entendimento
 - [x] C�digo Limpo
 - [x] Seguindo padr�o de escrita em CamelCase
 - [x] Baseado em Clean Architeture
 - [x] .Net 8
 - [x] C# 12
 - [x] Documenta��o
	 * <font color="orange">H� um erro na configura��o da documenta��o pois,  sempre ao pedir uma requisi��o est� pedindo a vers�o da API. </font>

# Autentica��o
* <font color="orange"> Prestar mais aten��o nas valida��es e informa��es ao usu�rio. S� descobri que o UserName deve ser um e-mail, porque olhei a valida��o, pois na doc n�o tem nada especificando. Havia colocado um nome e n�o passava. Mas foi colocado atributo de valida��o como e-mail. 
* Mensagens de retorno em qualquer situa��o para usu�rio, deve ser no idioma dele, no nosso caso em pt-BR.  Um pedido foi que usar nomes para a <font color="red"><strong>programa��o</strong></font> em ingl�s.
* Fiquei meio perdido em fazer o cadastro de um usu�rio para teste. Pois na l�gica posso cadastrar, pois o endpoint de Register-user est� anonymous, mas ao mesmo tempo, tem uma l�gica que a inclus�o somente pode ser feita por Administrator, mas como for�o a minha requisi��o em ser um Administrator? O ideal aqui � que o usu�rio base, fosse inserido na migrations, pelo menos para fins de desenvolvimento.  Como passei ao Caio o endpoint de Register-User, deve ser acessado somente depois de autenticado.  Esta pondera��o � somente para que possa lembrar disto em um futuro ok. Seguran�a sempre em 1o. lugar, depois da funcionalidade. 
*  Na classe UserService no m�todo RegisterUserAsync, temos a notifica��o que deveria estar em pt.Br, fiquemos atendo a isto.  Mas seu funcionamento est� ok. S� caio na l�gica de n�o ser admin.      
* <font color="red">Prestar aten��o  pois quase fica imposs�vel de fazer o registro do usu�rio, pois n�o h� valida��o da senha que � fraca e o identity n�o aceita por padr�o, ou seja, o servi�o deve fazer uso do objeto quando ele estiver v�lido. Sei que no caso do Identity ele tem as valida��es e posso trat�-lo como servi�o, mas o ideal que n�o dependamos de terceiros.  E o retorno do Identity vir� todo no idioma padr�o deles, melhor que fosse em pt-Br.

```json
          {
            "sucess": false,
            "errors": [
            "O CPF � inv�lido!",
            "Passwords must have at least one non alphanumeric character.",
            "Passwords must have at least one lowercase ('a'-'z').",
            "Passwords must have at least one uppercase ('A'-'Z')."
          ]
          }
```
* Cabe uma explica��o que os m�todos utilizados em `RegisterUserAsync`,  as valida��es, principalmente as de autentica��o, n�o s�o necess�rias, tantas valida��es, me d� impress�o que foram sendo colocadas para funcionar e n�o analisadas. Pois s� o atributo [Authorize] na controler j� iria eliminar a necessidade de verificar se est� autenticado. Outro ponto que autenticado ele j� viria com a roles no token ok.  


* ### Tudo isto que foi passado acima � s� para explica��o, nem tudo � erro, alguns pontos podem ter um custo mais alto, mas est�o funcionais.
</font>

# Usu�rios
*<font color="orange"> Get-All-Users-roles, deveria ter acesso s� com autentica��o. 

</font>

### <font color="red">N�o verifiquei tudo!
# Produtos
### <font color="red">N�o verifiquei tudo!
# Unidades
### <font color="red">N�o verifiquei tudo!
# Pessoas
### <font color="red">N�o verifiquei tudo!
# Valida��es
* <font color="orange">Valida��es no padr�o .net s�o funcionais e eficazes, mas  como temos recursos melhores como o FluentValidator, onde temos mais controles das mesmas, � melhor utiliz�-lo. N�o falei nada no desafio, mas seria um plus. O que fez est� correto, mas outros que usaram sempre o FluentValidator, ganhariam mais pontos ok. #ficadica
* ### <font color="red">Em todos m�todos nos servi�os que usam a valida��o, o ideal � mapear o request e n�o o objeto mapeado, isto porque, pode haver algum erro no request e n�o conseguir fazer o mapeamento, dando uma exception n�o tratada. Sempre validamos e estando v�lido, passamos adiante ok.  Atentar a isto.

</font>

# Utilit�rios

# Contextos
* <font color="red"><strong>Verificar para que Migrations rode somente em um ambiente que n�o em produ��o. Muito cuidado com isto. </strong>

</font>

# Logs
### N�o encontrei Arquivo de logs.

# Geral
* <font color="oranfe"> Sei que em muitos cursos os retornos da API s�o personalizados, mas caso n�o tenha sido pedido em um desafio ou mesmo  na documenta��o, o retorno deve ser sempre o objeto diretamente ou simplesmente o `IActionResul<object>`, assim temos um retorno padr�o do http. N�o est� errado o que fizeres ok. S� um dica. Apesar que neste desafio, deixei aberto para todos, o CustomResponse � uma boa sacada tamb�m, pois se for uma API espec�fica, podemos ter retornos bem espec�ficos. 
* 
### <font color="red"> M�todos get de qualquer outro endpoint, deve necessitar de autentica��o, pois, caso n�o tenha, qualquer usu�rio poder� acessar todos os dados. Seguran�a ok.</font>
</font>

# Avalia��o.
 ### <font color="blue"> No geral, ficou bem estrututurado. A ideia do desafio era usar os conhecimentos que aprendemos ao longo do m�s de dezembro, para que possamos utilizar e entender no geral o funcionamento. Claro que h� in�meros pontos que podemos melhorar na escrita do c�digo e claro baseado na minha experi�ncia e n�o que eu esteja certo, mas � mais funcional e o algoritimo mais barato. N�o consegui analisar todos os pontos, mas queria ver o que aprederam/entenderam e a capacidade de trabalharem em equipe para solucionar alguns problemas que hoje, no .Net Framework j� fariamos perfeitamente. No mais, estamos no caminho certo, quando iniciar o mobile, j� saber� como a resposta vem e o qu�o poder� ser dif�cil a API entregar os dados como o front quer. rsrsr
 
 # <font color="blue">Parab�ns!
> Written with [StackEdit](https://stackedit.io/).