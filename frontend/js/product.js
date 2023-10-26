let data = [
    {id: 1, category: "Shirt"},
    {id: 2, category: "Pants"},
    {id: 3, category: "Shoes"},
    {id: 4, category: "Hat"},
    {id: 5, category: "Dress"},
]

function readAll(){
    var tbdata = document.querySelector(".table_data")
    var elements = "";
    data.map(d => (
        elements += `<tr>
        <td>${d.category}</td>
        <td>
            <button onclick={edit(${d.id})}>Update</button>
            <button onclick={delet(${d.id})}>Delete</button>
        </td>
        </tr>`
    ))
    tbdata.innerHTML = elements;
}

function createForm(){
    document.querySelector(".create_form").style.display = "block";
    document.querySelector(".addbtn").style.display = "none";
}

function add(){
    var category = document.querySelector(".category").value;
    var newObj = {id: 6, category};
    data.push(newObj);

    document.querySelector(".create_form").style.display = "none";
    document.querySelector(".addbtn").style.display = "block";
    readAll();
}

function edit(id){
    document.querySelector(".update_form").style.display = "block";
    document.querySelector(".addbtn").style.display = "none";

    var updateObj = data.find(f => f.id === id);
    document.querySelector(".update_id").value = updateObj.id;
    document.querySelector('.ucategory').value = updateObj.category;
}

function update(){
    var id = parseInt(document.querySelector(".update_id").value);
    var category = document.querySelector('.ucategory').value;
    var updateObj = {id, category};

    var index = data.findIndex(f => f.id === id);
    data[index] = updateObj;
    document.querySelector(".update_form").style.display = "none";
    document.querySelector(".addbtn").style.display = "block";

    readAll();
}

function delet(id){
    var newdata = data.filter(f => f.id !== id);
    data = newdata;
    readAll();
}