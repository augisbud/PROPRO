// Function to create a table row
function createTableRow() {
    // Create a new table row
    var newRow = document.createElement('tr');

    // Create cells for the row
    var cell1 = document.createElement('td');
    cell1.textContent = document.getElementById('products[][name]').value || '';
    newRow.appendChild(cell1);

    var cell2 = document.createElement('td');
    cell2.textContent = document.getElementById('products[][quantity]').value || '';
    newRow.appendChild(cell2);

    var cell3 = document.createElement('td');
    cell3.textContent = document.getElementById('products[][quantity_type]').value || '';
    newRow.appendChild(cell3);

    var cell4 = document.createElement('td');
    cell4.textContent = document.getElementById('products[][price]').value || '';
    newRow.appendChild(cell4);

    var cell5 = document.createElement('td');
    cell5.textContent = document.getElementById('products[][sum]').value || '';
    newRow.appendChild(cell5);

    var cell6 = document.createElement('td');
    var deleteButton = document.createElement('a');
    deleteButton.className = 'btn btn-danger';
    deleteButton.textContent = 'Ištrinti';
    // Attach a click event listener to the delete button
    deleteButton.addEventListener('click', function() {
      // Remove the parent row when the delete button is clicked
      newRow.remove();
    });

    cell6.appendChild(deleteButton);
    newRow.appendChild(cell6);

    // Append the new row to the table
    document.getElementById('productsList').appendChild(newRow);
  }

  // Get the button element
  var addNewProduct = document.getElementById('addNewProduct');

  // Attach a click event listener to the button
  addNewProduct.addEventListener('click', createTableRow);