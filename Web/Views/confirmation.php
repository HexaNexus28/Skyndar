<!DOCTYPE html>
<html lang="fr">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Confirmation de réservation | &kilibre</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <link rel="stylesheet" href="../Styles/confirmation.css">
</head>

<body>
    <div class="container">
        <h1>Confirmation de votre réservation</h1>

        <div class="confirmation-message">
            <i class="fas fa-check-circle"></i> Merci, votre réservation a été confirmée avec succès.
        </div>

        <div class="reservation-details">
            <p><i class="far fa-calendar-alt"></i> <span class="highlight">Date :</span>
                <?php echo htmlspecialchars(date('d/m/Y', strtotime($creneau['date']))); ?></p>
            <p><i class="far fa-clock"></i> <span class="highlight">Horaire :</span>
                <?php echo htmlspecialchars($creneau['starthour'] . ' - ' . $creneau['endhour']); ?></p>

        </div>

        <div class="contact-info">
            <p>Pour toute question concernant votre rendez-vous :</p>
            <p>
                <i class="fas fa-envelope"></i> Contactez-moi à l'adresse :
                <a href="mailto:contact@e-ki-libre.net" class="email-link">contact@e-ki-libre.net</a>
            </p>
        </div>

        <p>Vous recevrez sous peu un e-mail de confirmation avec toutes les informations pratiques pour votre
            rendez-vous.</p>

        <a href="../Controllers/afficher_prestation.php" class="return-link">
            <i class="fas fa-arrow-left"></i> Retour à la page de réservation
        </a>
    </div>

    <footer>
        <p>&copy; 2025 &kilibre. Tous droits réservés.</p>
        <div class="social-icons">
            <a href="https://www.linkedin.com/in/bertrand-mangin/" aria-label="LinkedIn">
                <i class="fab fa-linkedin-in"></i>
            </a>
        </div>
    </footer>
</body>

</html>