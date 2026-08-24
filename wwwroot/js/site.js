(function () {
    "use strict";

    const digitSlots = Array.from(document.querySelectorAll(".digit-slot"));
    const digits = new Array(digitSlots.length).fill(null);
    let selectedIndex = 0;

    function renderSelection() {
        digitSlots.forEach((slot, i) => {
            slot.classList.toggle("active", i === selectedIndex);
        });
    }

    function renderDigits() {
        digitSlots.forEach((slot, i) => {
            const valueEl = slot.querySelector(".digit-value");
            valueEl.textContent = digits[i] ?? "–";
        });
    }

    function selectSlot(index) {
        selectedIndex = index;
        renderSelection();
    }

    function setDigit(value) {
        digits[selectedIndex] = value;
        renderDigits();
        // avanza automáticamente al siguiente casillero vacío
        const nextEmpty = digits.findIndex((d, i) => d === null && i > selectedIndex);
        if (nextEmpty !== -1) {
            selectSlot(nextEmpty);
        } else if (selectedIndex < digitSlots.length - 1) {
            selectSlot(selectedIndex + 1);
        }
    }

    function clearDigit() {
        digits[selectedIndex] = null;
        renderDigits();
    }

    digitSlots.forEach((slot, i) => {
        slot.addEventListener("click", () => selectSlot(i));
    });

    document.querySelectorAll(".key-btn[data-digit]").forEach((btn) => {
        btn.addEventListener("click", () => setDigit(parseInt(btn.dataset.digit, 10)));
    });

    document.getElementById("clearBtn").addEventListener("click", clearDigit);

    // ---- Modales de preguntas ----
    document.querySelectorAll(".question-btn").forEach((btn) => {
        btn.addEventListener("click", () => openModal(btn.dataset.modal));
    });

    document.querySelectorAll("[data-close]").forEach((el) => {
        el.addEventListener("click", () => closeModal(el.dataset.close));
    });

    document.querySelectorAll(".modal-overlay").forEach((overlay) => {
        overlay.addEventListener("click", (e) => {
            if (e.target === overlay) closeModal(overlay.id);
        });
    });

    function openModal(id) {
        document.getElementById(id)?.classList.add("open");
    }

    function closeModal(id) {
        document.getElementById(id)?.classList.remove("open");
    }

    // ---- Validación del código contra el servidor ----
    const statusEl = document.getElementById("lockStatus");

    document.getElementById("submitBtn").addEventListener("click", async () => {
        if (digits.some((d) => d === null)) {
            statusEl.textContent = "Completá los 5 dígitos antes de verificar.";
            return;
        }

        statusEl.textContent = "Verificando...";

        try {
            const response = await fetch("/Home/CheckCode", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ code: digits }),
            });

            const result = await response.json();

            digitSlots.forEach((slot, i) => {
                slot.classList.remove("right", "wrong");
                slot.classList.add(result.correctPositions[i] ? "right" : "wrong");
            });

            if (result.success) {
                statusEl.textContent = "¡Correcto!";
                setTimeout(() => openModal("modal-success"), 400);
            } else {
                statusEl.textContent = "Código incorrecto. Revisen las respuestas.";
            }
        } catch (err) {
            statusEl.textContent = "Error al verificar el código.";
            console.error(err);
        }
    });

    renderSelection();
    renderDigits();
})();
