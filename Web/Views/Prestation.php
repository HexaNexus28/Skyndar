<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">
    <link rel="stylesheet" href="../Styles/prestation.css">

    <script>
        setInterval(() => {
            location.reload();
        }, 5000);
    </script>
    <title>Prestation</title>
</head>

<body>
    <header class="header">
        <div class="header-content">
            <img src="../Images/logo.png" alt="Logo de &kilibre" class="logo">
            <h2 class="responsable">Bertrand Mangin</h2>
            <h6 class="subtitle">Rendez-vous</h6>
        </div>
    </header>

    <section class="container">

        <div class="card-container">

            <?php
            foreach ($Prestations as $Prestation) {
                echo '<div class="card">';
                echo '<a href="../Controllers/afficher_creneau.php?id=' . htmlspecialchars($Prestation['id']) . '">';
                echo '<h3>' . htmlspecialchars($Prestation['titre']) . '</h3>';
                echo '</a>';
                echo '<p>' . htmlspecialchars($Prestation['description']) . '</p>';
                echo '<p><strong><i class="fas fa-clock"></i> Durée : </strong>' . htmlspecialchars($Prestation['duree']) . ' minutes</p>';
                echo '<p><strong><i class="fas fa-map-marker-alt"></i> Lieu : </strong>Présentiel ou en visio-conférence</p>';
                echo '</div>';
            }
            ?>
        </div>
    </section>

    <footer class="footer">
        <div class="footer-content">
            <p>&copy; 2025 &kilibre. Tous droits réservés.</p>
            <div class="social-icons">
                <a href="https://www.linkedin.com/in/bertrand-mangin/"><i class="fab fa-linkedin-in"></i></a>
            </div>
        </div>
</body>

</html>