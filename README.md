# PROPRO


# Frontend Deployment
`sudo docker build -t invoices-frontend-image -f Dockerfile .`
`sudo docker run --name InvoicesFrontend -p 80:80 -d invoices-frontend-image`

# Backend Deployment
`sudo docker build -t invoices-backend-image -f Dockerfile .`
`sudo docker run --name InvoicesBackend --network internal -p 8080:8080 -d invoices-backend-image `