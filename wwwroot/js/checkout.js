function checkout(pubKey, sessionId) {
    const strip = Stripe(pubKey);
    stripe.redirectTOCheckout({sessionId});s
}