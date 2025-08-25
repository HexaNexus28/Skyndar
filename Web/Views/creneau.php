<!DOCTYPE html>
<html lang="fr">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="../Styles/creneau.css">
    <title>Calendrier de rendez-vous</title>
    <script>
        setInterval(() => {
            location.reload();
        }, 5000);
    </script>

</head>

<body>
    <header class="header">
        <div class="header-content">
            <img src="../Images/logo.png" alt="Logo de &kilibre" class="logo">
            <h2 class="responsable">Bertrand Mangin</h2>
            <h6 class="subtitle">Rendez-vous</h6>
        </div>
    </header>

    <div class="main-container">
        <div class="left-section">

            <div class="calendar-container">
                <div class="navigation">
                    <a href="afficher_creneau.php?month=<?php echo $prevmonth; ?>&year=<?php echo $prevyear; ?>&id=<?php echo $prestationId; ?>"
                        class="prev-month"><i class="fas fa-chevron-left">&lt;</i></a>
                    <?php
                    setlocale(LC_TIME, 'fr_FR.utf8', 'fra');
                    echo strftime('%B %Y', strtotime(sprintf('%04d-%02d-01', $currentyear, $currentmonth)));
                    ?>
                    <a href="afficher_creneau.php?month=<?php echo $nextmonth; ?>&year=<?php echo $nextyear; ?>&id=<?php echo $prestationId; ?>"
                        class="next-month"><i class="fas fa-chevron-right">&gt;</i></a>
                </div>

                <table border="1" cellpadding="8" cellspacing="0">
                    <tr>
                        <?php foreach ($daysOfWeek as $day) {
                            echo "<th>$day</th>";
                        } ?>
                    </tr>
                    <tbody>
                        <?php
                        $count = 0;
                        for ($i = 0; $i < 6; $i++) {
                            echo "<tr>";
                            for ($j = 0; $j < 7; $j++) {
                                $cell = $calendar[$count] ?? null;
                                if ($cell && isset($cell['date'])) {
                                    $date = $cell['date'];
                                    $dayNumber = date('d', strtotime($date));
                                    echo "<td><a href='?date=$date&month=$currentmonth&year=$currentyear&id=$prestationId'>$dayNumber</a></td>";
                                } else {
                                    echo "<td></td>";
                                }
                                $count++;
                            }
                            echo "</tr>";
                        }
                        ?>
                    </tbody>
                </table>
            </div>


            <div class="bottom-section">
                <div class="filter-options">
                    <h3>Filtres</h3>
                    <form method="get" action="../Controllers/afficher_creneau.php">
                        <input type="hidden" name="id" value="<?php echo htmlspecialchars($prestationId); ?>">
                        <input type="hidden" name="date" value="<?php echo htmlspecialchars($selectedDate); ?>">
                        <label>
                            <input type="checkbox" name="cabinet" value="cabinet">
                            Cabinet
                        </label>
                        <label>
                            <input type="checkbox" name="visio" value="visio">
                            Visio
                        </label>
                        <button type="submit">Appliquer les filtres</button>
                    </form>
                </div>

                <div class="map-container">
                    <iframe
                        src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2631.2028391038374!2d2.1223143156741004!3d48.82694737928457!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47e67dfb5b36b57f%3A0x4eb39785d0aad6f2!2sL&#39;Embelie!5e0!3m2!1sfr!2sfr!4v1720527370000!5m2!1sfr!2sfr"
                        width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy"
                        referrerpolicy="no-referrer-when-downgrade">
                    </iframe>

                </div>
            </div>
        </div>


        <div class="right-section">
            <h2 class="creneaux-title">Créneaux de la semaine du <?php echo $lundi; ?></h2>

            <?php
            for ($i = 0; $i < 7; $i++) {
                $jour = date('Y-m-d', strtotime("$lundi +$i days"));
                $day_id = getday_id($jour);

                $cabinetChecked = isset($_GET['cabinet']) && $_GET['cabinet'];
                $visioChecked = isset($_GET['visio']) && $_GET['visio'];
                $creneauxDuJour = getcreneaux($prestationId, $day_id);
                $filteredCreneaux = [];

                foreach ($creneauxDuJour as $c) {
                    if ($cabinetChecked && !$visioChecked && $c['cabinet']) {
                        $filteredCreneaux[] = $c;
                    } elseif (!$cabinetChecked && $visioChecked && !$c['cabinet']) {
                        $filteredCreneaux[] = $c;
                    } elseif (($cabinetChecked && $visioChecked) || (!$cabinetChecked && !$visioChecked)) {
                        $filteredCreneaux[] = $c;
                    }
                }

                echo "<h3 class='day-title'>" . date('d/m/Y', strtotime($jour)) . "</h3>";

                if ($filteredCreneaux) {
                    echo "<ul class='creneaux-list'>";
                    foreach ($filteredCreneaux as $creneau) {
                        $reservedCreneaux = getreservedcreneaux($creneau['id']);
                        $reserved = false;

                        foreach ($reservedCreneaux as $rc) {
                            if ($rc['starthour'] == $creneau['starthour']) {
                                echo "<li class='reserved'>{$creneau['starthour']} - {$creneau['endhour']} (Réservé)</li>";
                                $reserved = true;
                                break;
                            }
                        }

                        if (!$reserved) {
                            echo "<li><a href='afficher_formulaire.php?creneau_id={$creneau['id']}'>{$creneau['starthour']} - {$creneau['endhour']}</a></li>";
                        }
                    }
                    echo "</ul>";
                } else {
                    echo "<p class='no-creneaux'>Aucun créneau disponible</p>";
                }
            }
            ?>
        </div>
    </div>
</body>

</html>