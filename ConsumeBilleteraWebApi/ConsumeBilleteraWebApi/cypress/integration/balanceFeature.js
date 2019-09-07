/// <reference types="Cypress" />

context('Actions', () => {
    //beforeEach(() => {
    //    cy.visit('http://localhost:27246/Billeteras/Balance')
    //})


    it('GIVEN', () => {
        cy.visit('http://localhost:27246/Billeteras/Balance')
        cy.log('**AND**')
        cy.contains('Balance')
        cy.log('**AND**')
        cy.contains('Otra Transaccion')
            
    })

    it('WHEN', () => {
        cy.contains('Otra Transaccion').click()
        cy.log('**AND**')
        cy.get('#operacion').type('deposito')
        cy.log('**AND**')
        cy.get('#monto').type('10')
        cy.log('**AND**')
        cy.contains('Confirmar').click()
       
    })

    it('THEN', () => {
        cy.contains('Otra Transaccion')
    })

})