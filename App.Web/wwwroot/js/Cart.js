function Cart() {
        this.Cart = localStorage.getItem('Cart')
        this.Cart = this.Cart ? JSON.parse(this.Cart) : []
        this.Update = () => {
        localStorage.setItem('Cart', JSON.stringify(this.Cart))
        document.getElementById('CartValue').innerHTML = this.Cart.length;
        document.getElementById('CartAnchor').href = `/Cart?c=${JSON.stringify(this.Cart)}`
        }
        this.Update();
        this.FormatElementToInventory = (element,value) => {
            return { "ProductId": element.getAttribute('productid'), "LocationId": element.getAttribute('locationid'), "Qty": value }
        }
        this.AddToCart = (element) => {
            let input = element.previousElementSibling;
            let parsed = parseInt(input.value)
            let FormattedInput = this.FormatElementToInventory(element, parsed);
            if (parsed <= input.max && parsed > 0) {
                this.Cart.push(FormattedInput);
                console.log(localStorage.getItem('Cart'))
                this.Update();
                this.CreateRemoveButton(element)
                if (parsed == input.max) {
                    element.disabled = true;
                }
                input.max=input.max-parsed
            }
            else element.parentElement.querySelector('.cartInfo').innerHTML = "<p class='text-danger'>Please Enter a valid qty</p>"
        }
    this.UpdateCart = (element) => {
            console.log('activated')
            var input = element.previousElementSibling;
            for (let i = 0; i < this.Cart.length; i++) {
                let el = this.Cart[i];
                if (el.ProductId == element.getAttribute("productid")
                    && el.LocationId == element.getAttribute("locationid")) {
                    if (input.value > 0) {
                        let container = element.parentElement.parentElement;
                        let value = parseInt(input.value)
                        let max = parseInt(input.max)
                            if (value > max) {
                                input.value = input.max;
                                container.querySelector('.cartInfo').innerHTML = `Total Available Stock ${input.max}`
                                el.Qty = input.max
                            }
                            else if (value < max) {
                                el.Qty = input.value
                            }
                            container.querySelector('.Qty').innerHTML = el.Qty;
                            let price = parseInt(container.querySelector('.Price').innerText.slice(1));
                            let total = container.querySelector('.Total')
                            let previous = parseInt(total.innerText.slice(1))
                        let difference = parseInt(el.Qty * price - previous)
                        total.innerText = '$' + el.Qty * price;
                        let totals = [...document.querySelectorAll('.Total')]
                            .map(el => parseInt(el.innerText.slice(1).replace(',', '')))
                            .reduce((a,b)=>a+=b,0)
                        let order_total = document.getElementById("OrderTotal")

                            order_total.innerText = "$"+totals
                            break;
                        }
                        else {
                            element.parentElement.parentElement.remove();
                            this.Cart.splice(i, 1);
                        }
                    }
                }
                this.Update();
    }
    this.Modal = (data) => {
        let _ = `
<div style="display:block; background: #0000007d;" class="modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">New Order Created: <a href="/Orders/Details/${data.orderNumber}">${data.orderNumber}</h5>
      </div>
      <div class="modal-body">
        <p>There are ${data.failedOrders.length} failed insertions:</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>`
        document.body.insertAdjacentHTML('afterbegin', _);
    }
    this.Checkout = (element) => {
        var CustomerId = document.querySelector('.CustomerName').value;
        if (CustomerId == "") {
            return document.querySelector('.CheckOutWarning').innerHTML="Please Select a valid customer before checking out"
        }
        const data = JSON.stringify({ CustomerId: CustomerId, Items: this.Cart });
        console.log(data);

        var d = fetch('/Orders/Checkout', {
            method: 'POST', // or 'PUT'
            headers: {
                'Content-Type': 'application/json',
            },
            body: data,
        })
            .then(response => response.json())
            .then(data => {
                this.Modal(data)
            })
            .catch((error) => {
                console.error('Error:', error);
            });
        this.Cart = [];
        this.Update()
    }
    this.UpdateBrowsingInformation= (element, maxvalue)=> {
        let _ = element.querySelector('input')
        _.max = _.max + maxvalue
        let inputButton = element.querySelector('button')
        if (inputButton.disabled == true) inputButton.disabled=false
    }
    this.CreateRemoveButton = (element,index) => {
            var index = index?index:this.Cart.length-1
            let _ = document.createElement('button')
            _.classList.add('btn-danger')
            _.innerText = "Remove From Cart"
            _.onclick = () => {
                var max = this.Cart[index].Qty
                this.Cart.splice(index - 1, 1);
                _.parentElement.removeChild(_);
                this.UpdateBrowsingInformation(element.parentNode,max)
                this.Update();
            }
            let container = element.parentNode.querySelector('.cartInfo');
            container.innerHTML = '';
            container.appendChild(_);
        }
        this.Actions = {
            ".AddToCart": this.AddToCart,
            ".UpdateCart":this.UpdateCart
        }
    if (CartRegisterActions) {
        document.querySelectorAll(CartRegisterActions).forEach(el => el.onclick = () => this.Actions[CartRegisterActions](el))
        if (CartRegisterActions == ".UpdateCart") {
            var _ = document.querySelector('.Checkout')
            _.onclick = () => this.Checkout(_)
        }
        if (CartRegisterActions == ".AddToCart") {
            this.Cart.forEach((el, i) => {
                let f = [...document.querySelectorAll('.AddToCart')].filter(f => f.getAttribute("productId") == el.ProductId && f.getAttribute("locationid") == el.LocationId);
                f = f[0]
                if (f) {
                    let I = f.parentElement.querySelector('input');
                    I.max -= el.Qty;
                    I.value = el.Qty;
                    this.CreateRemoveButton(f, i);
                }
            })
        }
    }
    }
    _cart=Cart()