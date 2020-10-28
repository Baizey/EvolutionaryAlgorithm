class Input {
    constructor(id) {
        this._row = document.getElementById('row_' + id);
        this._input = document.getElementById('input_' + id);
    }

    show() {
        this._row.classList.remove('hidden');
    }

    hide() {
        this._row.classList.add('hidden');
    }

    set(value) {
        this._input.value = value;
    }

    get value() {
        return this._input.value;
    }
}