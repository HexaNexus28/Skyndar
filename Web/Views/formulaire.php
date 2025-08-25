<!DOCTYPE html>
<html lang="fr">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Formulaire de réservation | &kilibre</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <link rel="stylesheet" href="../Styles/formulaire.css">

</head>

<body>
    <div class="form-container">
        <h2><i class="far fa-calendar-check"></i> Informations personnelles</h2>

        <form action="../Controllers/confirmation.php" method="get">
            <div class="form-group">
                <label class="required-field">Nom</label>
                <input type="text" name="nom" required placeholder="Votre nom de famille"
                    aria-label="Votre nom de famille">
            </div>

            <div class="form-group">
                <label class="required-field">Prénom</label>
                <input type="text" name="prenom" required placeholder="Votre prénom" aria-label="Votre prénom">
            </div>

            <div class="form-group">
                <label class="required-field">Email</label>
                <input type="email" name="email" required placeholder="votre@email.com"
                    aria-label="Votre adresse email">
            </div>

            <input type="hidden" name="creneau_id" value="<?php echo htmlspecialchars($creneau_id); ?>">

            <button type="submit">
                <i class="fas fa-check-circle"></i> Confirmer la réservation
            </button>

            <div class="privacy-notice">
                <p><i class="fas fa-lock"></i> <strong>Protection de vos données</strong></p>
                <p>Les informations que vous nous communiquez sont uniquement utilisées pour gérer votre rendez-vous.
                    Conformément au RGPD, nous ne partageons pas vos données avec des tiers et nous les conservons
                    uniquement
                    pendant la durée nécessaire à la réalisation de votre rendez-vous.</p>
            </div>
        </form>
    </div>
</body>

</html>