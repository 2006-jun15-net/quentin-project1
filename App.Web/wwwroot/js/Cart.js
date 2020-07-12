function Cart() {
        this.Cart = localStorage.getItem('Cart')
        this.Cart = this.Cart ? JSON.parse(this.Cart) : []
        this.Update = () => {
        localStorage.setItem('Cart', JSON.stringify(this.Cart))
        document.getElementById('CartValue').innerHTML = this.Cart.length;
        document.getElementById('CartAnchor').href = `/Cart?c=${JSON.stringify(this.Cart)}`
        }
        this.Update();
        this.AddToCart = (element) => {
            var input = element.previousElementSibling;
            if (parseInt(input.value) <= input.max) {
                this.Cart.push({ "ProductId": element.getAttribute('productid'), "LocationId": element.getAttribute('locationid'), "Qty": input.value });
                console.log(localStorage.getItem('Cart'))
                this.Update();
                this.CreateRemoveButton(element)
            }
            else element.nextElementSibling.innerHTML="Please Enter a valid qty"
        }
    this.UpdateCart = (element) => {
            console.log('activated')
            var input = element.previousElementSibling;
            for (let i = 0; i < this.Cart.length; i++) {
                let el = this.Cart[i];
                if (el.ProductId == element.getAttribute("productid") && el.LocationId == element.getAttribute("locationid")) {
                    if (input.value > 0) {
                            el.Qty = input.value;
                            element.parentElement.parentElement.querySelector('.Qty').innerHTML = input.value;
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
        this.CreateRemoveButton=(element)=> {
            let _ = document.createElement('button')
            _.innerText = "Remove From Cart"
            _.onclick = () => {
                this.Cart.splice(this.Cart.length - 1, 1);
                _.parentElement.removeChild(_);
                this.Update();
            }
            element.parentNode.append(_);
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
    }
    }
    _cart=Cart()