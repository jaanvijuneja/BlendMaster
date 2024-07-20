document.addEventListener('DOMContentLoaded', () => {
    if (localStorage.getItem('loggedUser')) {
        const sidebarHTML = `
          <div class="sidebar">
            <ul class="nav flex-column">
              <li class="nav-item">
                <a class="nav-link" id="tab1" data-bs-toggle="tab" href="/chat">Chat</a>
              </li>
              <li class="nav-item">
                <a class="nav-link" id="tab2" data-bs-toggle="tab" href="#content2">Tab 2</a>
              </li>
              <li class="nav-item">
                <a class="nav-link" id="tab3" data-bs-toggle="tab" href="#content3">Tab 3</a>
              </li>
            </ul>
          </div>
        `;
        const sidebarContainer = document.createElement('div');
        sidebarContainer.innerHTML = sidebarHTML;
        document.body.appendChild(sidebarContainer);
    }
});