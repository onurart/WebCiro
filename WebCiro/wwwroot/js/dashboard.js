"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();
setInterval(function () {
	$('#TR-01 rect').css('fill', 'red');
}, 2000); // 2000 milisaniye = 2 saniye
$(function () {
	connection.start().then(function () {

		
		InvokeSales();
	
	}).catch(function (err) {
		return console.error(err.toString());
	});
});


// Sale
function InvokeSales() {
	connection.invoke("SendSales").catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceivedSales", function (sales) {
	BindSalesToGrid(sales);
});

function BindSalesToGrid(sales) {
	function processSale(index) {
		if (index < sales.length) {
			var sale = sales[index];

			const formatter = new Intl.NumberFormat('tr-TR', {
				style: 'currency',
				currency: 'TRY',
				minimumFractionDigits: 0,
			});

			const counter = document.getElementById('countSale');

			const animate = () => {
				console.log(counter)
				let num1 = Number(counter.innerText.replace(/[^\d.-]/g, ''))
				let num2 = sale.amount;
				let sum = num1 + num2;

				let cntr = 0;
				var interval = setInterval(function () {
					var randomNum = Math.floor(Math.random() * 100000);
					counter.innerHTML = randomNum;
					cntr++;
					if (cntr === 20) {
						clearInterval(interval);
						counter.innerHTML = " "
						counter.innerHTML = formatter.format(sum);
					}
				}, 100);

			}




			let invokeModal = $('#invokeModal');
			let wrapInvokeModal = $('#wrapInkoveModal');

			var tr = $('<tr/>');
			tr.append(`<td id="upper-case">${sale.City}</td>`);
			tr.append(`<td>${sale.amount} <span>&#8378;</span> </td>`);
			tr.append(`<td>${sale.productName}</td>`);
			tr.append(`<td>${sale.produtGruop}</td>`);
			$('#tblSale tbody').prepend(tr); // Use prepend to add the new row at the beginning

			// Check if sale.branch matches any data-city-name in SVG paths
			$('path[data-city-name]').each(function () {
				var cityName = $(this).attr('data-city-name');
				if (cityName === sale.City) {
					var pathElement = $(this);

					// Apply orange color to the current SVG path
					pathElement.addClass('blink');
					wrapInvokeModal.addClass('blink');
					animate();


					// Set a timeout to revert the color back to the original after 3 seconds
					setTimeout(function () {
						// Remove the blink class
						pathElement.removeClass('blink');
						wrapInvokeModal.removeClass('blink');

						// Process the next sale after 10 seconds

						processSale(index + 1);
					}, 7500); // 10 seconds

					return false; // Break out of the loop after the first match
				}
			});
			function firstLetterUpper(text) {
				const result = text[0].toUpperCase() + text.slice(1);
				return result
			}
			invokeModal.empty()
			invokeModal.append(`<div id="product-city"> İl: ${firstLetterUpper(sale.City)}</div>`)
			invokeModal.append(` <div id="product-title"> Kategori: ${sale.produtGruop}</div>`)
			invokeModal.append(`<div id="product-price"> Tutar: ${sale.amount} ₺</div>`)
		}
	}

	// Start processing the first sale
	processSale(0);
}







































//// Product
//function InvokeProducts() {
//	connection.invoke("SendProducts").catch(function (err) {
//		return console.error(err.toString());
//	});
//}

//connection.on("ReceivedProducts", function (products) {
//	BindProductsToGrid(products);
//});

//function BindProductsToGrid(products) {
//	$('#tblProduct tbody').empty();

//	var tr;
//	$.each(products, function (index, product) {
//		tr = $('<tr/>');
//		tr.append(`<td>${(index + 1)}</td>`);
//		tr.append(`<td>${product.name}</td>`);
//		tr.append(`<td>${product.category}</td>`);
//		tr.append(`<td>${product.price}</td>`);
//		$('#tblProduct').append(tr);
//	});
//}

//connection.on("ReceivedProductsForGraph", function (productsForGraph) {
//	BindProductsToGraph(productsForGraph);
//});




////function InvokeCustomer() {
////	connection.invoke("MapCustomer").catch(function (err) {
////		return console.error(err.toString());
////	});
////}

////connection.on("ReceivedMapCusmert", function (sales) {
////	BindMapCusmertoGrid(sales);
////});

////function BindMapCusmertoGrid(sales) {
////	function processSale(index) {
////		if (index < sales.length) {
////			var sale = sales[index];
////			//$('#countSaleWrap').find("span").remove();

////			const formatter = new Intl.NumberFormat('tr-TR', {
////				style: 'currency',
////				currency: 'TRY',
////			});

////			const counter = document.getElementById('countSale');
////			const speed = 200;


////			const animate = () => {
////				const value = +sale.amount;
////				const data = +counter.innerText;

////				const time = value / speed;
////				if (data < value) {
////					counter.innerText = Math.ceil(data + time);
////					setTimeout(animate, 1);
////				} else {
////					counter.innerText = formatter.format(value);
////				}

////			}

////			animate();
////		}
////	}
////	// Start processing the first sale
////	processSale(0);
////}

















////function InvokeAntep() {
////	connection.invoke("MapAntep").catch(function (err) {
////		return console.error(err.toString());
////	});
////}

////connection.on("ReceivedMapAntep", function (sales) {
////	BindAntepGrid(sales);
////});

////function BindAntepGrid(sales) {
////	function processSale(index) {
////		if (index < sales.length) {
////			var sale = sales[index];
	
////			$('#countantep').find("span").remove();

////			$('#countantep').append(`<span id="countSale">${sale.branch} <span>&#8378;</span> </span>`); // Use prepend to add the new row at the beginning
////		}
////	}

////	// Start processing the first sale
////	processSale(0);
////}






////function InvokeSamsun() {
////	connection.invoke("MapSamsun").catch(function (err) {
////		return console.error(err.toString());
////	});
////}

////connection.on("ReceivedMapSamsun", function (sales) {
////	BindSamsunGrid(sales);
////});

////function BindSamsunGrid(sales) {
////	function processSale(index) {
////		if (index < sales.length) {
////			var sale = sales[index];
		
////			$('#countSamsun').find("span").remove();

////			$('#countSamsun').append(`<span id="countSale">${sale.branch} <span>&#8378;</span> </span>`); // Use prepend to add the new row at the beginning
////		}
////	}

////	// Start processing the first sale
////	processSale(0);
////}



//connection.on("ReceivedSalesForGraph", function (salesForGraph) {
//	BindSalesToGraph(salesForGraph);
//});

//function BindSalesToGraph(salesForGraph) {
//	var labels = [];
//	var data = [];

//	$.each(salesForGraph, function (index, item) {
//		labels.push(item.purchasedOn);
//		data.push(item.amount);
//	});

//	DestroyCanvasIfExists('canvasSales');

//	const context = $('#canvasSales');
//	const myChart = new Chart(context, {
//		type: 'line',
//		data: {
//			labels: labels,
//			datasets: [{
//				label: '# of Sales',
//				data: data,
//				backgroundColor: backgroundColors,
//				borderColor: borderColors,
//				borderWidth: 1
//			}]
//		},
//		options: {
//			scales: {
//				y: {
//					beginAtZero: true
//				}
//			}
//		}
//	});
//}

//// Customer
//function InvokeCustomers() {
//	connection.invoke("SendCustomers").catch(function (err) {
//		return console.error(err.toString());
//	});
//}

//connection.on("ReceivedCustomers", function (customers) {
//	BindCustomersToGrid(customers);
//});

//function BindCustomersToGrid(customers) {
//	$('#tblCustomer tbody').empty();

//	var tr;
//	$.each(customers, function (index, customer) {
//		tr = $('<tr/>');
//		tr.append(`<td>${(index + 1)}</td>`);
//		tr.append(`<td>${customer.name}</td>`);
//		tr.append(`<td>${customer.gender}</td>`);
//		tr.append(`<td>${customer.mobile}</td>`);
//		$('#tblCustomer').append(tr);
//	});
//}

//connection.on("ReceivedCustomersForGraph", function (customersForGraph) {
//	BindCustomersToGraph(customersForGraph);
//});

//function BindCustomersToGraph(customersForGraph) {
//	var datasets = [];
//	var labels = ['Customers']
//	var data = [];
//	$.each(customersForGraph, function (index, item) {
//		data = [];
//		data.push(item.customers);
//		var dataset = {
//			label: item.gender,
//			data: data,
//			backgroundColor: backgroundColors[index],
//			borderColor: borderColors[index],
//			borderWidth: 1
//		};

//		datasets.push(dataset);
//	});

//	DestroyCanvasIfExists('canvasCustomers');

//	const context = $('#canvasCustomers');
//	const myChart = new Chart(context, {
//		type: 'bar',
//		data: {
//			labels: labels,
//			datasets: datasets,
//		},
//		options: {
//			scales: {
//				y: {
//					beginAtZero: true
//				}
//			}
//		}
//	});
//}

//// supporting functions for Graphs
//function DestroyCanvasIfExists(canvasId) {
//	let chartStatus = Chart.getChart(canvasId);
//	if (chartStatus != undefined) {
//		chartStatus.destroy();
//	}
//}

//var backgroundColors = [
//	'rgba(255, 99, 132, 0.2)',
//	'rgba(54, 162, 235, 0.2)',
//	'rgba(255, 206, 86, 0.2)',
//	'rgba(75, 192, 192, 0.2)',
//	'rgba(153, 102, 255, 0.2)',
//	'rgba(255, 159, 64, 0.2)'
//];
//var borderColors = [
//	'rgba(255, 99, 132, 1)',
//	'rgba(54, 162, 235, 1)',
//	'rgba(255, 206, 86, 1)',
//	'rgba(75, 192, 192, 1)',
//	'rgba(153, 102, 255, 1)',
//	'rgba(255, 159, 64, 1)'
//];





/**********************************  Eski ********************************************** */

//"use strict";

//var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

//$(function () {
//    connection.start().then(function () {
//		alert('Connected to dashboardHub');

//		InvokeProducts();
//		InvokeSales();
//		InvokeCustomers();

//    }).catch(function (err) {
//        return console.error(err.toString());
//    });
//});

//// Product
//function InvokeProducts() {
//	connection.invoke("SendProducts").catch(function (err) {
//		return console.error(err.toString());
//	});
//}

//connection.on("ReceivedProducts", function (products) {
//	BindProductsToGrid(products);
//});

//function BindProductsToGrid(products) {
//	$('#tblProduct tbody').empty();

//	var tr;
//	$.each(products, function (index, product) {
//		tr = $('<tr/>');
//		tr.append(`<td>${(index + 1)}</td>`);
//		tr.append(`<td>${product.name}</td>`);
//		tr.append(`<td>${product.category}</td>`);
//		tr.append(`<td>${product.price}</td>`);
//		$('#tblProduct').append(tr);
//	});
//}

//connection.on("ReceivedProductsForGraph", function (productsForGraph) {
//	BindProductsToGraph(productsForGraph);
//});

//function BindProductsToGraph(productsForGraph) {
//	var labels = [];
//	var data = [];

//	$.each(productsForGraph, function (index, item) {
//		labels.push(item.category);
//		data.push(item.products);
//	});

//	DestroyCanvasIfExists('canvasProudcts');

//	const context = $('#canvasProudcts');
//	const myChart = new Chart(context, {
//		type: 'doughnut',
//		data: {
//			labels: labels,
//			datasets: [{
//				label: '# of Products',
//				data: data,
//				backgroundColor: backgroundColors,
//				borderColor: borderColors,
//				borderWidth: 1
//			}]
//		},
//		options: {
//			scales: {
//				y: {
//					beginAtZero: true
//				}
//			}
//		}
//	});
//}

//// Sale
//function InvokeSales() {
//	connection.invoke("SendSales").catch(function (err) {
//		return console.error(err.toString());
//	});
//}

//connection.on("ReceivedSales", function (sales) {
//	BindSalesToGrid(sales);
//});

//function BindSalesToGrid(sales) {
//	$('#tblSale tbody').empty();

//	var tr;
//	$.each(sales, function (index, sale) {
//		tr = $('<tr/>');
//		tr.append(`<td>${sale.id}</td>`);
//		tr.append(`<td>${sale.branch}</td>`);
//		tr.append(`<td>${sale.customer}</td>`);
//		tr.append(`<td>${sale.ProductName}</td>`);
//		tr.append(`<td>${sale.produtgruop}</td>`);
//		tr.append(`<td>${sale.amount}</td>`);
//		tr.append(`<td>${sale.purchasedOn}</td>`);
//	$('#tblSale').append(tr);
//	});
//}

//connection.on("ReceivedSalesForGraph", function (salesForGraph) {
//	BindSalesToGraph(salesForGraph);
//});

//function BindSalesToGraph(salesForGraph) {
//	var labels = [];
//	var data = [];

//	$.each(salesForGraph, function (index, item) {
//		labels.push(item.purchasedOn);
//		data.push(item.amount);
//	});

//	DestroyCanvasIfExists('canvasSales');

//	const context = $('#canvasSales');
//	const myChart = new Chart(context, {
//		type: 'line',
//		data: {
//			labels: labels,
//			datasets: [{
//				label: '# of Sales',
//				data: data,
//				backgroundColor: backgroundColors,
//				borderColor: borderColors,
//				borderWidth: 1
//			}]
//		},
//		options: {
//			scales: {
//				y: {
//					beginAtZero: true
//				}
//			}
//		}
//	});
//}

//// Customer
//function InvokeCustomers() {
//	connection.invoke("SendCustomers").catch(function (err) {
//		return console.error(err.toString());
//	});
//}

//connection.on("ReceivedCustomers", function (customers) {
//	BindCustomersToGrid(customers);
//});

//function BindCustomersToGrid(customers) {
//	$('#tblCustomer tbody').empty();

//	var tr;
//	$.each(customers, function (index, customer) {
//		tr = $('<tr/>');
//		tr.append(`<td>${(index + 1)}</td>`);
//		tr.append(`<td>${customer.name}</td>`);
//		tr.append(`<td>${customer.gender}</td>`);
//		tr.append(`<td>${customer.mobile}</td>`);
//		$('#tblCustomer').append(tr);
//	});
//}

//connection.on("ReceivedCustomersForGraph", function (customersForGraph) {
//	BindCustomersToGraph(customersForGraph);
//});

//function BindCustomersToGraph(customersForGraph) {
//	var datasets = [];
//	var labels = ['Customers']
//	var data = [];
//	$.each(customersForGraph, function (index, item) {
//		data = [];
//		data.push(item.customers);
//		var dataset = {
//			label: item.gender,
//			data: data,
//			backgroundColor: backgroundColors[index],
//			borderColor: borderColors[index],
//			borderWidth: 1
//		};

//		datasets.push(dataset);
//	});

//	DestroyCanvasIfExists('canvasCustomers');

//	const context = $('#canvasCustomers');
//	const myChart = new Chart(context, {
//		type: 'bar',
//		data: {
//			labels: labels,
//			datasets: datasets,
//		},
//		options: {
//			scales: {
//				y: {
//					beginAtZero: true
//				}
//			}
//		}
//	});
//}

//// supporting functions for Graphs
//function DestroyCanvasIfExists(canvasId) {
//	let chartStatus = Chart.getChart(canvasId);
//	if (chartStatus != undefined) {
//		chartStatus.destroy();
//	}
//}

//var backgroundColors = [
//	'rgba(255, 99, 132, 0.2)',
//	'rgba(54, 162, 235, 0.2)',
//	'rgba(255, 206, 86, 0.2)',
//	'rgba(75, 192, 192, 0.2)',
//	'rgba(153, 102, 255, 0.2)',
//	'rgba(255, 159, 64, 0.2)'
//];
//var borderColors = [
//	'rgba(255, 99, 132, 1)',
//	'rgba(54, 162, 235, 1)',
//	'rgba(255, 206, 86, 1)',
//	'rgba(75, 192, 192, 1)',
//	'rgba(153, 102, 255, 1)',
//	'rgba(255, 159, 64, 1)'
//];